using VRCScalerOSC.Controller;
using VRCScalerOSC.ViewModel;
using VRCScalerOSC.View;

namespace VRCScalerOSC_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string defaultSettingPath = "VRCScalerOSC.Setting.txt";
            Controller_Scaler controller = new(Controller_ScalerSetting.ImportCustomSetting(defaultSettingPath), new ViewModel_Scaler());
            try
            {
                if (!File.Exists(defaultSettingPath))
                {
                    var viewModel = controller.ViewModelScaler;
                    Controller_ScalerSetting.ExportCustomSetting(controller.CustomSetting, defaultSettingPath, $"{ViewModel_Scaler.AppVersion.Major}.{ViewModel_Scaler.AppVersion.Minor}.{ViewModel_Scaler.AppVersion.Build}");
                }
            }
            catch { }
            ScalerCosole scaler = new(controller);
            scaler.Run();
        }
    }
}
