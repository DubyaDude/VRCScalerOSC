using VRCScalerOSC.ViewModel;
using VRCScalerOSC.Controller;

namespace VRCScalerOSC_Windows
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            string defaultSettingPath = "VRCScalerOSC.Setting.txt";
            Controller_Scaler controller = new(Controller_ScalerSetting.ImportCustomSetting(defaultSettingPath), new ViewModel_Scaler());

            ApplicationConfiguration.Initialize();
            Application.Run(new ScalerForm(controller));
        }
    }
}