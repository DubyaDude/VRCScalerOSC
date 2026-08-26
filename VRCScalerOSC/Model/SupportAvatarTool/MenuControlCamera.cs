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

        public override void InitOSCFunctions(OscEventCollection functions)
        {
            functions.AddEvent("/usercamera/Mode", (_, service, data) =>
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
            functions.AddEvent("/usercamera/Close", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Capture", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/CaptureDelayed", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/TriggerTakesPhotos", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/DollyPathsStayVisible", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/RollWhileFlying", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/GreenScreen", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Lock", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/OrientationIsLandscape", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Flying", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/SmoothMovement", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/AutoLevelRoll", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/AutoLevelPitch", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/ShowUIInCamera", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/LocalPlayer", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/RemotePlayer", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Environment", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Streaming", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/ShowFocus", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/AudioFromCamera", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/LookAtMe", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Zoom", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Exposure", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/FocalDistance", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Aperture", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Hue", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Saturation", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Lightness", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/FlySpeed", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/TurnSpeed", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/SmoothingStrength", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/PhotoRate", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Duration", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/LookAtMeXOffset", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/LookAtMeYOffset", SetOSCDataToMenuControlCameraViewModel);
            functions.AddEvent("/usercamera/Pose", SetOSCDataToMenuControlCameraViewModel);

            functions.AddEvent($"{oscPathPrefix}/Mode", (IsInitialized, service, data) =>
            {
                if (IsInitialized && data.ValueI.HasValue)
                {
                    if (data.ValueI.Value > 0)
                    {
                        service?.SendOscMessage(new OSCData("/usercamera/Mode", "i", data.ValueI.Value));
                    }
                }
            });
            functions.AddEvent($"{oscPathPrefix}/Close", TransferButtonData);
            functions.AddEvent($"{oscPathPrefix}/Capture", TransferButtonData);
            functions.AddEvent($"{oscPathPrefix}/CaptureDelayed", TransferButtonData);
            functions.AddEvent($"{oscPathPrefix}/TriggerTakesPhotos", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/DollyPathsStayVisible", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/RollWhileFlying", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/GreenScreen", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/Lock", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/OrientationIsLandscape", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/Flying", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/SmoothMovement", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/AutoLevelRoll", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/AutoLevelPitch", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/ShowUIInCamera", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/LocalPlayer", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/RemotePlayer", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/Environment", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/Streaming", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/ShowFocus", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/AudioFromCamera", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/LookAtMe", TransferToggleData);
            functions.AddEvent($"{oscPathPrefix}/Zoom", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/Exposure", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/FocalDistance", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/Aperture", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/Hue", TransferValueData2);
            functions.AddEvent($"{oscPathPrefix}/Saturation", TransferValueData2);
            functions.AddEvent($"{oscPathPrefix}/Lightness", TransferValueData2);
            functions.AddEvent($"{oscPathPrefix}/FlySpeed", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/TurnSpeed", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/SmoothingStrength", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/PhotoRate", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/Duration", TransferValueData);
            functions.AddEvent($"{oscPathPrefix}/LookAtMeOffsetPuppetOn", (IsInitialized, service, data) =>
            {
                if (data.ValueB.HasValue && !data.ValueB.Value)
                {
                    _timer1.Change(Timeout.Infinite, Timeout.Infinite);
                    _timer1Runing = false;
                }
            });
            functions.AddEvent($"{oscPathPrefix}/LookAtMeXOffset", (IsInitialized, service, data) =>
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
            functions.AddEvent($"{oscPathPrefix}/LookAtMeYOffset", (IsInitialized, service, data) =>
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
