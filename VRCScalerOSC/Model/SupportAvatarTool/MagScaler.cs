using VRCScalerOSC.Controller;
using VRCScalerOSC.Service;
using VRCScalerOSC.ViewModel;

namespace VRCScalerOSC.Model.SupportAvatarTool
{
    public class MagScaler(Controller_Scaler controller, ViewModel_Scaler viewModel) : SupportAvatarTool
    {
        public override void InitOSCFunctions(OscEventCollection supportAbatarToolOSCFuns)
        {
            supportAbatarToolOSCFuns.AddEvent("/avatar/parameters/ScaleFactorInverse", (isInitialized, service, data) =>
            {
                if (data.ValueF.HasValue)
                    DefaultEyeHeight = EyeHeightAsMeters * data.ValueF.Value;
            });
            supportAbatarToolOSCFuns.AddEvent("/avatar/parameters/EyeHeightAsMeters", (isInitialized, service, data) =>
            {
                if (data.ValueF.HasValue)
                    DefaultEyeHeight = data.ValueF.Value * ScaleFactorInverse;
            });
        }
        public override Action<bool, Service_VRCOSCProtocols?, OSCData>? TryAddNewFunction(OSCData initialData)
        {
            if (initialData.Addr.StartsWith("/avatar/parameters/", StringComparison.Ordinal))
            {
                string param = initialData.Addr.ToString().Replace("/avatar/parameters/", "");
                if (param.Contains("ScaleOverlay") || param.Contains("NoReadyReset") || param.Contains("SelectAScale"))
                {
                    return (isInitialized, service, data) =>
                    {
                        if (isInitialized && data.ValueF.HasValue && data.ValueF.Value > 0.5f)
                        {
                            service?.SendOscMessage(new OSCData("/avatar/parameters/" + param, data.TypeString, 0));
                        }
                    };
                }
                else if (param.Contains("NextScale"))
                {
                    return (IsInitialized, service, data) =>
                    {
                        if (data.ValueI.HasValue)
                        {
                            float NextEyeHeight = DefaultEyeHeight;
                            switch (data.ValueI.Value)
                            {
                                //case 0: NextEyeHeight *= 0.01f; break;
                                case 25: NextEyeHeight *= 0.05f; break;
                                case 36: NextEyeHeight *= 0.1f; break;
                                case 47: NextEyeHeight *= 0.2f; break;
                                case 55: NextEyeHeight *= 0.33f; break;
                                case 62: NextEyeHeight *= 0.5f; break;
                                case 65: NextEyeHeight *= 0.6f; break;
                                case 69: NextEyeHeight *= 0.8f; break;
                                //case 73: NextEyeHeight *= 1f; break;
                                case 95: NextEyeHeight *= 4f; break;
                                case 112: NextEyeHeight *= 12f; break;
                                case 120: NextEyeHeight *= 20f; break;
                                case 130: NextEyeHeight *= 36f; break;
                                case 135: NextEyeHeight *= 50f; break;
                                case 141: NextEyeHeight *= 75f; break;
                                case 146: NextEyeHeight *= 100f; break;
                                case 157: NextEyeHeight *= 200f; break;
                                case 163: NextEyeHeight *= 300f; break;
                                case 168: NextEyeHeight *= 400f; break;
                                case 172: NextEyeHeight *= 530f; break;
                                case 175: NextEyeHeight *= 640f; break;
                                case 178: NextEyeHeight *= 770f; break;
                                case 180: NextEyeHeight *= 870f; break;
                                case 182: NextEyeHeight *= 1000f; break;
                                case 193: NextEyeHeight *= 2000f; break;
                                case 200: NextEyeHeight *= 3000f; break;
                                case 204: NextEyeHeight *= 4000f; break;
                                case 208: NextEyeHeight *= 5100f; break;
                                case 211: NextEyeHeight *= 6200f; break;
                                case 214: NextEyeHeight *= 7500f; break;
                                case 217: NextEyeHeight *= 9000f; break;
                                case 219: NextEyeHeight *= 10000f; break;
                                case 230: NextEyeHeight *= 20000f; break;
                                case 236: NextEyeHeight *= 30000f; break;
                                case 240: NextEyeHeight *= 40000f; break;
                                case 245: NextEyeHeight *= 53000f; break;
                                case 248: NextEyeHeight *= 64000f; break;
                                case 251: NextEyeHeight *= 78000f; break;
                                case 253: NextEyeHeight *= 88000f; break;
                                case 255: NextEyeHeight *= 100000f; break;
                                default: return;
                            }
                            if (NextEyeHeight < 0.01f)
                            {
                                NextEyeHeight = 0.01f;
                            }
                            if (NextEyeHeight > 10000)
                            {
                                NextEyeHeight = 10000;
                            }
                            controller.StartScaling(false, viewModel.FixedRate, NextEyeHeight, viewModel.ScalingTime, viewModel.ScalingRate);
                        }
                    };
                }
                else if (param.Contains("Scaled"))
                {
                    return (IsInitialized, service, data) =>
                    {
                        if (data.ValueI.HasValue)
                        {
                            float NextEyeHeight = DefaultEyeHeight;
                            switch (data.ValueI.Value)
                            {
                                case 31: NextEyeHeight *= 0.01f; break;
                                case 35: NextEyeHeight *= 0.03f; break;
                                case 38: NextEyeHeight *= 0.07f; break;
                                case 40: NextEyeHeight *= 0.11f; break;
                                case 42: NextEyeHeight *= 0.17f; break;
                                case 45: NextEyeHeight *= 0.33f; break;
                                case 46: NextEyeHeight *= 0.41f; break;
                                case 48: NextEyeHeight *= 0.64f; break;
                                //case 50: NextEyeHeight *= 1f; break;
                                case 51: NextEyeHeight *= 1.25f; break;
                                case 52: NextEyeHeight *= 1.6f; break;
                                case 54: NextEyeHeight *= 2.5f; break;
                                case 56: NextEyeHeight *= 4f; break;
                                case 58: NextEyeHeight *= 6f; break;
                                case 60: NextEyeHeight *= 9f; break;
                                case 61: NextEyeHeight *= 12f; break;
                                case 62: NextEyeHeight *= 15f; break;
                                case 63: NextEyeHeight *= 18f; break;
                                case 64: NextEyeHeight *= 23f; break;
                                case 66: NextEyeHeight *= 36f; break;
                                case 68: NextEyeHeight *= 56f; break;
                                case 69: NextEyeHeight *= 69f; break;
                                case 71: NextEyeHeight *= 108f; break;
                                case 73: NextEyeHeight *= 169f; break;
                                case 74: NextEyeHeight *= 212f; break;
                                case 76: NextEyeHeight *= 331f; break;
                                case 79: NextEyeHeight *= 646f; break;
                                case 81: NextEyeHeight *= 1010f; break;
                                case 83: NextEyeHeight *= 1578f; break;
                                case 85: NextEyeHeight *= 2465f; break;
                                case 88: NextEyeHeight *= 4815f; break;
                                case 91: NextEyeHeight *= 9404f; break;
                                default: return;
                            }
                            if (NextEyeHeight < 0.01f)
                            {
                                NextEyeHeight = 0.01f;
                            }
                            if (NextEyeHeight > 10000)
                            {
                                NextEyeHeight = 10000;
                            }
                            controller.StartScaling(false, viewModel.FixedRate, NextEyeHeight, viewModel.ScalingTime, viewModel.ScalingRate);
                        }
                    };
                }
            }
            return null;
        }
    }
}
