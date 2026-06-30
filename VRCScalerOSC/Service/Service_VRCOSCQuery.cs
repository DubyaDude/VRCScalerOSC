using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text.Json.Nodes;
using VRC.OSCQuery;
using VRCScalerOSC.Model;

namespace VRCScalerOSC.Service
{

    public class Service_VRCOSCQuery : IDisposable
    {
        private OSCQueryService? _service;
        private static readonly HttpClient _httpClient = new() { Timeout = TimeSpan.FromSeconds(5) };
        public int? TcpPort { get { return _service?.TcpPort; } }
        public int? OscPort { get { return _service?.OscPort; } }
        public string? HostIP { get { return _service?.HostIP.ToString(); } }
        public string? OscIP { get { return _service?.OscIP.ToString(); } }
        public Dictionary<string, OSCQueryServiceProfile> ProfileList = [];
        public List<string> IgnoreProfileList = [];
        public string? CurrentAvatarId = "";
        public delegate void EventHandler(OSCQueryServiceProfile? profile, List<OSCData> data);
        public event EventHandler? VRCDatareceived;

        ~Service_VRCOSCQuery()
        {
            RemoveEndPoint();
        }

        public void Dispose()
        {
            RemoveEndPoint();
        }

        public int ReStart(IPAddress LocalIp, int UdpPort = 9001)
        {
            IgnoreProfileList.Clear();
            ProfileList.Clear();
            Stop();
            _service = new OSCQueryServiceBuilder()
                .WithServiceName("VRCScalerOSC")
                .WithUdpPort(UdpPort <= 0 ? FindPortInRange(9010, 9100) : UdpPort)
                .WithTcpPort(GetAvailablePort())
                .WithHostIP(LocalIp)
                .WithOscIP(LocalIp)
                .AddListenerForServiceType(Service_OnOscQueryServiceAdded, OSCQueryServiceProfile.ServiceType.OSCQuery)
                .WithDefaults()
                .Build();
            RemoveEndPoint();
            AddEndPoint();
            return _service.OscPort;
        }

        private void Service_OnOscQueryServiceAdded(OSCQueryServiceProfile obj)
        {
            if (VRCDatareceived != null && obj.name.Contains("VRChat-Client") && GetVRCInitOSCData(obj, out List<OSCData> oOSCDataList))
            {
                string AvatarId = oOSCDataList.Find((data) => { return data.Addr == "/avatar/change"; })?.ValueString ?? "";
                if (oOSCDataList.Count > 0)
                {
                    ProfileList.TryAdd(obj.name, obj);
                    CurrentAvatarId = AvatarId;
                    if (oOSCDataList.Count > 0)
                    {
                        VRCDatareceived.Invoke(obj, oOSCDataList);
                    }
                }
            }
        }
        public void Stop()
        {
            RemoveEndPoint();
            _service?.Dispose();
        }

        private void AddEndPoint()
        {
            _service?.AddEndpoint<string>("/avatar/change", Attributes.AccessValues.WriteOnly);
            _service?.AddEndpoint<int>("/avatar/parameters/*", Attributes.AccessValues.ReadWrite);
            _service?.AddEndpoint<bool>("/avatar/parameters/*", Attributes.AccessValues.ReadWrite);
            _service?.AddEndpoint<float>("/avatar/parameters/*", Attributes.AccessValues.ReadWrite);
            _service?.AddEndpoint<bool>("/avatar/eyeheightscalingallowed", Attributes.AccessValues.WriteOnly);
            _service?.AddEndpoint("/tracking/vrsystem/head/pose", "ffffff", Attributes.AccessValues.WriteOnly);
            _service?.AddEndpoint("/tracking/vrsystem/leftwrist/pose", "ffffff", Attributes.AccessValues.WriteOnly);
            _service?.AddEndpoint("/tracking/vrsystem/rightwrist/pose", "ffffff", Attributes.AccessValues.WriteOnly);
            _service?.AddEndpoint<int>("/usercamera/*", Attributes.AccessValues.ReadWrite);
            _service?.AddEndpoint<bool>("/usercamera/*", Attributes.AccessValues.ReadWrite);
            _service?.AddEndpoint<float>("/usercamera/*", Attributes.AccessValues.ReadWrite);
            _service?.AddEndpoint("/usercamera/Pose", "ffffff", Attributes.AccessValues.WriteOnly);
        }
        private void RemoveEndPoint()
        {
            _service?.RemoveEndpoint("/avatar/change");
            _service?.RemoveEndpoint("/avatar/parameters/*");
            _service?.RemoveEndpoint("/avatar/parameters/*");
            _service?.RemoveEndpoint("/avatar/parameters/*");
            _service?.RemoveEndpoint("/avatar/eyeheightscalingallowed");
            _service?.RemoveEndpoint("/tracking/vrsystem/head/pose");
            _service?.RemoveEndpoint("/tracking/vrsystem/leftwrist/pose");
            _service?.RemoveEndpoint("/tracking/vrsystem/rightwrist/pose");
            _service?.RemoveEndpoint("/usercamera/*");
            _service?.RemoveEndpoint("/usercamera/*");
            _service?.RemoveEndpoint("/usercamera/*");
            _service?.RemoveEndpoint("/usercamera/Pose");
        }
        static int GetAvailablePort()
        {
            TcpListener l = new(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }
        public static int FindPortInRange(int startPort, int endPort)
        {
            Random random = new();
            List<int> portList = [];
            for (int port = startPort; port <= endPort; port++)
            {
                portList.Add(port);
            }
            for (int i = 0; i < (endPort - startPort) * 10; i++)
            {
                int a = random.Next(portList.Count);
                int b = random.Next(portList.Count);
                (portList[b], portList[a]) = (portList[a], portList[b]);
            }
            foreach (int port in portList)
            {
                try
                {
                    TcpListener l = new(IPAddress.Any, port);
                    l.Start();
                    l.Stop();
                    return port;
                }
                catch (SocketException)
                {
                    continue;
                }
            }
            return GetAvailablePort();
        }

        public bool GetVRCInitOSCData(OSCQueryServiceProfile profile, out List<OSCData> returnOSCDataList)
        {
            returnOSCDataList = [];
            if (IgnoreProfileList.Contains(profile.name))
            {
                return false;
            }
            try
            {
                string url = $"http://{profile.address}:{profile.port}/";
                string jsonResponse = _httpClient.GetStringAsync(url).Result;
                if (jsonResponse != null)
                {
                    returnOSCDataList = ParseOscTree(jsonResponse);
                }
                return true;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is HttpRequestException)
                {
                    IgnoreProfileList.Add(profile.name);
                }
                Debug.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public static List<OSCData> ParseOscTree(string jsonResponse)
        {
            var resultList = new List<OSCData>();
            var root = JsonNode.Parse(jsonResponse);
            TraverseNode(root, "", resultList);
            return resultList;
        }
        private static void TraverseNode(JsonNode? node, string currentPath, List<OSCData> resultList)
        {
            try
            {
                if (node is JsonObject obj)
                {
                    if (obj["TYPE"] != null && obj["VALUE"] is JsonArray jArray && jArray.Count > 0)
                    {
                        string typeString = obj["TYPE"]?.ToString() ?? string.Empty;
                        switch (typeString)
                        {
                            case "T":
                                if (jArray.Count > 0)
                                {
                                    if (jArray[0]?.GetValue<bool>() ?? false)
                                    {
                                        resultList.Add(new OSCData(currentPath, "T", true));
                                    }
                                    else
                                    {
                                        resultList.Add(new OSCData(currentPath, "F", false));
                                    }
                                }
                                break;
                            case "F":
                                resultList.Add(new OSCData(currentPath, typeString, false));
                                break;
                            case "s":
                                if (jArray.Count > 0)
                                {
                                    resultList.Add(new OSCData(currentPath, typeString, jArray[0]?.GetValue<string>() ?? string.Empty));
                                }
                                break;
                            case "i":
                                if (jArray.Count > 0)
                                {
                                    resultList.Add(new OSCData(currentPath, typeString, jArray[0]?.GetValue<int>() ?? 0));
                                }
                                break;
                            case "f":
                                if (jArray.Count > 0)
                                {
                                    if (!(jArray[0]?.ToString() == "NaN"))
                                    {
                                        resultList.Add(new OSCData(currentPath, typeString, jArray[0]?.GetValue<float>() ?? 0f));
                                    }
                                }
                                break;
                            case "ff":
                            case "fff":
                            case "ffff":
                            case "ffffff":
                                if (jArray.Count == 0 || jArray[0] is not JsonArray jArray1 || jArray1.Count == 0)
                                {
                                    break;
                                }

                                float[] floats = new float[jArray1.Count];
                                for (int i = 0; i < jArray1.Count; i++)
                                {
                                    floats[i] = jArray1[i]?.GetValue<float>() ?? 0f;
                                }

                                switch (floats.Length)
                                {
                                    case 2:
                                        resultList.Add(new OSCData(currentPath, typeString, new Vector2(floats[0], floats[1])));
                                        break;
                                    case 3:
                                        resultList.Add(new OSCData(currentPath, typeString, new Vector3(floats[0], floats[1], floats[2])));
                                        break;
                                    case 4:
                                        resultList.Add(new OSCData(currentPath, typeString, new Vector4(floats[0], floats[1], floats[2], floats[3])));
                                        break;
                                    case 6:
                                        resultList.Add(new OSCData(currentPath, typeString, new PoseData(floats[0], floats[1], floats[2], floats[3], floats[4], floats[5])));
                                        break;
                                }
                                break;
                        }
                    }
                    if (obj["CONTENTS"] is JsonObject contents)
                    {
                        foreach (var property in contents)
                        {
                            string nextPath = $"{currentPath}/{property.Key}";
                            TraverseNode(property.Value, nextPath, resultList);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"OSC Parse Error: {e.Message}");
            }
        }
    }
}

