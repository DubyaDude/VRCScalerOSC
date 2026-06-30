using VRCScalerOSC.Service;
using VRCScalerOSC.ViewModel;

namespace VRCScalerOSC.Model.SupportAvatarTool
{
    public class MenuControlCamera(string oscPathPrefix, ViewModel_MCC viewModel) : SupportAvatarTool
    {
        private Timer _timer1 = new((state) => { }, null, Timeout.Infinite, Timeout.Infinite);
        private Timer _timer2 = new((state) => { }, null, Timeout.Infinite, Timeout.Infinite);
        private bool _timer1Runing = false;
        private bool _timer2Runing = false;
        private float _lookAtMeXOffsetPuppet = 0f;
        private float _lookAtMeYOffsetPuppet = 0f;
        private readonly PoseData _modifyPoseData = new(0f, 0f, 0f, 0f, 0f, 0f);

        public override void InitOSCFunctions(Dictionary<string, Action<bool, Service_VRCOSCProtocols?, OSCData>>.AlternateLookup<ReadOnlySpan<char>> functions)
        {
            functions.TryAdd("/usercamera/Mode", (_, service, data) =>
            {
                if (data.ValueI.HasValue)
                {
                    viewModel.Mode = data.ValueI.Value;
                    if (viewModel.Mode != 0)
                    {
                        service?.IgnoreAddrListRemoveByKeyWord("/usercamera/");
                    }
                }
            });
            functions.TryAdd("/usercamera/Close", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Capture", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/CaptureDelayed", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/TriggerTakesPhotos", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/DollyPathsStayVisible", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/RollWhileFlying", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/GreenScreen", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Lock", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/OrientationIsLandscape", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Flying", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/SmoothMovement", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/AutoLevelRoll", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/AutoLevelPitch", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/ShowUIInCamera", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/LocalPlayer", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/RemotePlayer", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Environment", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Streaming", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/ShowFocus", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/AudioFromCamera", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/LookAtMe", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Zoom", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Exposure", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/FocalDistance", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Aperture", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Hue", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Saturation", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Lightness", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/FlySpeed", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/TurnSpeed", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/SmoothingStrength", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/PhotoRate", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Duration", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/LookAtMeXOffset", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/LookAtMeYOffset", SetOSCDataToMenuControlCameraViewModel);
            functions.TryAdd("/usercamera/Pose", SetOSCDataToMenuControlCameraViewModel);

            functions.TryAdd($"{oscPathPrefix}/Mode", (IsInitialized, service, data) =>
            {
                if (IsInitialized && data.ValueI.HasValue)
                {
                    if (data.ValueI.Value > 0)
                    {
                        service?.SendOscMessage(new OSCData("/usercamera/Mode", "i", data.ValueI.Value));
                    }
                }
            });
            functions.TryAdd($"{oscPathPrefix}/Close", TransferButtonData);
            functions.TryAdd($"{oscPathPrefix}/Capture", TransferButtonData);
            functions.TryAdd($"{oscPathPrefix}/CaptureDelayed", TransferButtonData);
            functions.TryAdd($"{oscPathPrefix}/TriggerTakesPhotos", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/DollyPathsStayVisible", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/RollWhileFlying", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/GreenScreen", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/Lock", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/OrientationIsLandscape", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/Flying", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/SmoothMovement", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/AutoLevelRoll", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/AutoLevelPitch", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/ShowUIInCamera", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/LocalPlayer", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/RemotePlayer", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/Environment", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/Streaming", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/ShowFocus", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/AudioFromCamera", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/LookAtMe", TransferToggleData);
            functions.TryAdd($"{oscPathPrefix}/Zoom", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/Exposure", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/FocalDistance", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/Aperture", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/Hue", TransferValueData2);
            functions.TryAdd($"{oscPathPrefix}/Saturation", TransferValueData2);
            functions.TryAdd($"{oscPathPrefix}/Lightness", TransferValueData2);
            functions.TryAdd($"{oscPathPrefix}/FlySpeed", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/TurnSpeed", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/SmoothingStrength", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/PhotoRate", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/Duration", TransferValueData);
            functions.TryAdd($"{oscPathPrefix}/LookAtMeOffsetPuppetOn", (IsInitialized, service, data) =>
            {
                if (data.ValueB.HasValue && !data.ValueB.Value)
                {
                    _timer1.Change(Timeout.Infinite, Timeout.Infinite);
                    _timer1Runing = false;
                }
            });
            functions.TryAdd($"{oscPathPrefix}/LookAtMeXOffset", (IsInitialized, service, data) =>
            {
                if (data.ValueF.HasValue)
                {
                    _lookAtMeXOffsetPuppet = data.ValueF.Value;
                    if (_lookAtMeXOffsetPuppet > 0.3 || _lookAtMeXOffsetPuppet < -0.3)
                    {
                        if (!_timer1Runing)
                        {

                            _timer1Runing = true;
                            _timer1.Change(Timeout.Infinite, Timeout.Infinite);
                            _timer1 = new Timer((seate) =>
                            {
                                service?.SendOscMessage(new OSCData("/usercamera/LookAtMeXOffset", "f", LCO(viewModel.LookAtMeXOffset - _lookAtMeXOffsetPuppet * -0.5f, "LookAtMeXOffset")));
                            }, null, 0, 50);
                        }
                    }
                    else
                    {
                        _timer1.Change(Timeout.Infinite, Timeout.Infinite);
                        _timer1Runing = false;
                    }
                }
            });
            functions.TryAdd($"{oscPathPrefix}/LookAtMeYOffset", (IsInitialized, service, data) =>
            {
                if (data.ValueF.HasValue)
                {
                    _lookAtMeYOffsetPuppet = data.ValueF.Value;
                    if (_lookAtMeYOffsetPuppet > 0.3 || _lookAtMeYOffsetPuppet < -0.3)
                    {
                        if (!_timer2Runing)
                        {
                            _timer2Runing = true;
                            _timer2.Change(Timeout.Infinite, Timeout.Infinite);
                            _timer2 = new Timer((seate) =>
                            {
                                service?.SendOscMessage(new OSCData("/usercamera/LookAtMeYOffset", "f", LCO(viewModel.LookAtMeYOffset - _lookAtMeYOffsetPuppet * 0.5f, "LookAtMeYOffset")));
                            }, null, 0, 50);
                        }
                    }
                    else
                    {
                        _timer2.Change(Timeout.Infinite, Timeout.Infinite);
                        _timer2Runing = false;
                    }
                }
            });
        }
        private void SetOSCDataToMenuControlCameraViewModel(bool isInitialized, Service_VRCOSCProtocols? service, OSCData data)
        {
            if ((isInitialized && viewModel.Mode == 0))
            {
                service?.IgnoreAddrListAdd(data);
            }
            else if (!SetOSCDataToViewModel(data))
            {
                service?.IgnoreAddrListAdd(data);
            }
            else if (!isInitialized && data.IsBool && data.ValueB.HasValue)
            {
                if (data.ValueB.Value)
                {
                    service?.SendOscMessage(OSCData.GetTrueOSCData($"{oscPathPrefix}{data.Addr["/usercamera".Length..]}", "T"));
                }
                else
                {
                    service?.SendOscMessage(OSCData.GetFalseOSCData($"{oscPathPrefix}{data.Addr["/usercamera".Length..]}", "F"));
                }
            }
        }
        private void TransferButtonData(bool IsInitialized, Service_VRCOSCProtocols? service, OSCData data)
        {
            if (IsInitialized)
            {
                if (data.ValueB.HasValue && data.ValueB.Value)
                {
                    service?.SendOscMessage(OSCData.GetTrueOSCData("/usercamera" + data.Addr[oscPathPrefix.Length..].ToString(), "T"));
                    service?.SendOscMessage(OSCData.GetFalseOSCData(data.Addr[oscPathPrefix.Length..].ToString(), data.TypeString));
                }
            }
        }
        private void TransferToggleData(bool IsInitialized, Service_VRCOSCProtocols? service, OSCData data)
        {
            if (IsInitialized)
            {
                if (data.ValueB.HasValue && data.ValueB.Value)
                {
                    service?.SendOscMessage(OSCData.GetTrueOSCData("/usercamera" + data.Addr[oscPathPrefix.Length..].ToString(), "T"));
                }
                else if (data.ValueB.HasValue && !data.ValueB.Value)
                {
                    service?.SendOscMessage(OSCData.GetFalseOSCData("/usercamera" + data.Addr[oscPathPrefix.Length..].ToString(), "F"));
                }
            }
        }
        private void TransferValueData(bool IsInitialized, Service_VRCOSCProtocols? service, OSCData data)
        {
            if (IsInitialized && data.ValueF.HasValue)
            {
                string param = data.Addr[oscPathPrefix.Length..].ToString().Trim('/');
                float valueF = data.ValueF.Value;
                if (data.ValueF.Value > 0.5f)
                {
                    valueF = (valueF - 0.5f) * 2f * (ParamLimit[param][2] - ParamLimit[param][0]) + ParamLimit[param][0];
                }
                else if (data.ValueF.Value < 0.5f)
                {
                    valueF = valueF * 2f * (ParamLimit[param][0] - ParamLimit[param][1]) + ParamLimit[param][1];
                }
                else
                {
                    valueF = ParamLimit[param][0];
                }
                service?.SendOscMessage(new OSCData("/usercamera/" + param, "f", LCO(valueF, param)));
            }
        }
        private void TransferValueData2(bool IsInitialized, Service_VRCOSCProtocols? service, OSCData data)
        {
            if (IsInitialized && data.ValueF.HasValue)
            {
                string param = data.Addr[oscPathPrefix.Length..].ToString().Trim('/');
                float valueF = data.ValueF.Value;
                valueF = valueF * (ParamLimit[param][2] - ParamLimit[param][1]) + ParamLimit[param][1];
                service?.SendOscMessage(new OSCData("/usercamera/" + param, "f", LCO(valueF, param)));
            }
        }
        public override Action<bool, Service_VRCOSCProtocols?, OSCData>? TryAddNewFunction(OSCData initialData)
        {
            return null;
        }
        public bool SetOSCDataToViewModel(OSCData data)
        {
            switch (data.Addr)
            {
                case "/usercamera/Mode":
                    if (data.ValueI.HasValue) viewModel.Mode = data.ValueI.Value;
                    return true;
                case "/usercamera/Close":
                case "/usercamera/Capture":
                case "/usercamera/CaptureDelayed":
                    return false;
                case "/usercamera/TriggerTakesPhotos":
                    viewModel.TriggerTakesPhotos = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/DollyPathsStayVisible":
                    viewModel.DollyPathsStayVisible = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/RollWhileFlying":
                    viewModel.RollWhileFlying = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/GreenScreen":
                    viewModel.GreenScreen = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/Lock":
                    viewModel.Lock = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/OrientationIsLandscape":
                    viewModel.OrientationIsLandscape = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/Flying":
                    viewModel.Flying = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/SmoothMovement":
                    viewModel.SmoothMovement = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/AutoLevelRoll":
                    viewModel.AutoLevelRoll = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/AutoLevelPitch":
                    viewModel.AutoLevelPitch = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/ShowUIInCamera":
                    viewModel.ShowUIInCamera = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/LocalPlayer":
                    viewModel.LocalPlayer = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/RemotePlayer":
                    viewModel.RemotePlayer = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/Environment":
                    viewModel.Environment = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/Streaming":
                    viewModel.Streaming = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/ShowFocus":
                    viewModel.ShowFocus = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/AudioFromCamera":
                    viewModel.AudioFromCamera = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/LookAtMe":
                    viewModel.LookAtMe = data.ValueB.HasValue && data.ValueB.Value; return true;
                case "/usercamera/Zoom":
                    if (data.ValueF.HasValue) viewModel.Zoom = data.ValueF.Value;
                    return true;
                case "/usercamera/Exposure":
                    if (data.ValueF.HasValue) viewModel.Exposure = data.ValueF.Value;
                    return true;
                case "/usercamera/FocalDistance":
                    if (data.ValueF.HasValue) viewModel.FocalDistance = data.ValueF.Value;
                    return true;
                case "/usercamera/Aperture":
                    if (data.ValueF.HasValue) viewModel.Aperture = data.ValueF.Value;
                    return true;
                case "/usercamera/Hue":
                    if (data.ValueF.HasValue) viewModel.Hue = data.ValueF.Value;
                    return true;
                case "/usercamera/Saturation":
                    if (data.ValueF.HasValue) viewModel.Saturation = data.ValueF.Value;
                    return true;
                case "/usercamera/Lightness":
                    if (data.ValueF.HasValue) viewModel.Lightness = data.ValueF.Value;
                    return true;
                case "/usercamera/FlySpeed":
                    if (data.ValueF.HasValue) viewModel.FlySpeed = data.ValueF.Value;
                    return true;
                case "/usercamera/TurnSpeed":
                    if (data.ValueF.HasValue) viewModel.TurnSpeed = data.ValueF.Value;
                    return true;
                case "/usercamera/SmoothingStrength":
                    if (data.ValueF.HasValue) viewModel.SmoothingStrength = data.ValueF.Value;
                    return true;
                case "/usercamera/PhotoRate":
                    if (data.ValueF.HasValue) viewModel.PhotoRate = data.ValueF.Value;
                    return true;
                case "/usercamera/Duration":
                    if (data.ValueF.HasValue) viewModel.Duration = data.ValueF.Value;
                    return true;
                case "/usercamera/LookAtMeXOffset":
                    if (data.ValueF.HasValue) viewModel.LookAtMeXOffset = data.ValueF.Value;
                    return true;
                case "/usercamera/LookAtMeYOffset":
                    if (data.ValueF.HasValue) viewModel.LookAtMeYOffset = data.ValueF.Value;
                    return true;
                case "/usercamera/Pose":
                    if (data.IsVectorPoseData && data.ValuePose != null)
                    {
                        viewModel.Pose = data.ValuePose;
                    }
                    return true;
            }
            return false;
        }

        private readonly Dictionary<string, float[]> ParamLimit = new()
        {
            //Default, Minimum, Maximum
            {"Zoom",              new float[]{45f,20f,300f} },
            {"Exposure",          new float[]{0f,-10f,4f} },
            {"FocalDistance",     new float[]{1.5f,0f,10f} },
            {"Aperture",          new float[]{15f,1.4f,32f} },
            {"Hue",               new float[]{360f,0f,360f} },
            {"Saturation",        new float[]{100f,0f,100f} },
            {"Lightness",         new float[]{50f,0f,50f} },
            {"LookAtMeXOffset",   new float[]{0f,-25f,25f} },
            {"LookAtMeYOffset",   new float[]{0f,-25f,25f} },
            {"FlySpeed",          new float[]{3f,0.1f,15f} },
            {"TurnSpeed",         new float[]{1f,0.1f,5f} },
            {"SmoothingStrength", new float[]{5f,0.1f,10f} },
            {"PhotoRate",         new float[]{1f,0.1f,2f} },
            {"Duration",          new float[]{2f,0.1f,60f} },
        };

        private float LCO(float value, string param)
        {
            if (value <= ParamLimit[param][1])
            {
                return ParamLimit[param][1];
            }
            if (value >= ParamLimit[param][2])
            {
                return ParamLimit[param][2];
            }
            return value;
        }

        private void ModifyPose(Service_VRCOSCProtocols? service)
        {

            _modifyPoseData.PX = viewModel.Pose.PX + ((Cos(viewModel.Pose.RY) * Cos(viewModel.Pose.RZ) + Sin(viewModel.Pose.RY) * Sin(viewModel.Pose.RX) * Sin(viewModel.Pose.RZ)) * viewModel.CtrlPose.PX * viewModel.ModifyRate)
                                                             + ((-Cos(viewModel.Pose.RY) * Sin(viewModel.Pose.RZ) + Sin(viewModel.Pose.RY) * Sin(viewModel.Pose.RX) * Cos(viewModel.Pose.RZ)) * viewModel.CtrlPose.PY * viewModel.ModifyRate)
                                                             + ((Sin(viewModel.Pose.RY) * Cos(viewModel.Pose.RX)) * viewModel.CtrlPose.PZ * viewModel.ModifyRate);
            _modifyPoseData.PY = viewModel.Pose.PY + ((Cos(viewModel.Pose.RX) * Sin(viewModel.Pose.RZ)) * viewModel.CtrlPose.PX * viewModel.ModifyRate)
                                                         + ((Cos(viewModel.Pose.RX) * Cos(viewModel.Pose.RZ)) * viewModel.CtrlPose.PY * viewModel.ModifyRate)
                                                         + ((-Sin(viewModel.Pose.RX)) * viewModel.CtrlPose.PZ * viewModel.ModifyRate);
            _modifyPoseData.PZ = viewModel.Pose.PZ + ((-Sin(viewModel.Pose.RY) * Cos(viewModel.Pose.RZ) + Cos(viewModel.Pose.RY) * Sin(viewModel.Pose.RX) * Sin(viewModel.Pose.RZ)) * viewModel.CtrlPose.PX * viewModel.ModifyRate)
                                                         + ((Sin(viewModel.Pose.RY) * Sin(viewModel.Pose.RZ) + Cos(viewModel.Pose.RY) * Sin(viewModel.Pose.RX) * Cos(viewModel.Pose.RZ​)) * viewModel.CtrlPose.PY * viewModel.ModifyRate)
                                                         + ((Cos(viewModel.Pose.RY) * Cos(viewModel.Pose.RX​)) * viewModel.CtrlPose.PZ * viewModel.ModifyRate);
            _modifyPoseData.RX = FixRotation(viewModel.Pose.RY + Math.Sign(viewModel.CtrlPose.RX));
            _modifyPoseData.RY = FixRotation180(viewModel.Pose.RX - Math.Sign(viewModel.CtrlPose.RY));
            _modifyPoseData.RZ = FixRotation(viewModel.Pose.RZ - Math.Sign(viewModel.CtrlPose.RZ));
            Span<byte> span = "/usercamera/Pose\0\0\0\0,ffffff\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0"u8.ToArray();
            int offset = 28;
            OSCData.FloatToBytes(ref span, ref offset, [_modifyPoseData.PX, _modifyPoseData.PY, _modifyPoseData.PZ, _modifyPoseData.RX, _modifyPoseData.RY, _modifyPoseData.RZ]);
            service?.SendOscMessage(new OSCData(span.ToArray()));
        }
        private static float Cos(float d)
        {
            return MathF.Cos(d * MathF.PI / 180f);
        }
        private static float Sin(float d)
        {
            return MathF.Sin(d * MathF.PI / 180f);
        }
        private static float FixRotation(float d)
        {
            if (d > 360f)
            {
                return d - 360f;
            }
            if (d < 0f)
            {
                return d + 360f;
            }
            return d;
        }
        private static float FixRotation180(float d)
        {
            if (d > 360f)
            {
                d -= 360f;
            }
            if (d < 0f)
            {
                d += 360f;
            }
            if (d > 180f && d < 270f)
            {
                return 269.9f;
            }
            if (d < 180f && d > 90f)
            {
                return 89.9f;
            }
            return d;
        }
    }
}
