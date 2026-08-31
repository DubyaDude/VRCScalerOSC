using VRCScalerOSC.Service;

namespace VRCScalerOSC.Model.SupportAvatarTool
{
    public abstract class SupportAvatarTool()
    {
        public float DefaultEyeHeight = 1;
        public float ScaleFactorInverse = 1;
        public float EyeHeightAsMeters = 1;
        public abstract void InitOSCFunctions(OscEventCollection functions);
        public abstract Action<bool, Service_VRCOSCProtocols?, OSCData>? TryAddNewFunction(OSCData initialData);
    }
}
