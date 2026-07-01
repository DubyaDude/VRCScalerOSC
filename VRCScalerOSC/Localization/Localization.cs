namespace VRCScalerOSC.Localization
{
    public class Localization
    {
        public enum LocalizationId
        {
            None = 0,
            enUS = 1,
            jaJP = 2,
            koKR = 3,
            zhTW = 4,
            zhCN = 5,
        }
        protected enum Key
        {
            GroupBoxHeight,
            LabelHeightRange,
            HeightRangeVRChatAvatar,
            HeightRangeVRChatWorld,
            HeightRangeAdvanced,
            HeightRangeLimit,
            HeightRangeUserSettings,
            HeightRangeSetUpper,
            HeightRangeSetLower,
            ButtonResetHeight,
            ButtonResetHeightLite,
            ButtonChangeScale,
            ButtonStop,
            GroupBoxScalingTime,
            GroupBoxScalingRate,
            LabelSec,
            LabelRate,
            LabelMeters,
            LabelMultiplier,
            CheckBoxAutoAbort,
            GroupBoxSetting,
            ButtonLanguage,
            ButtonFormSize,
            GroupBoxOSCConfig,
            LabelOSCIP,
            LabelOSCSendPort,
            LabelOSCReceivePort,
            CheckBoxOSCRandomReceiverPort,
            OSCStop,
            OSCStart,
            ButtonOSCSetup,
            LabelCEH,
            LabelDEH,
            LabelSF,
            LabelGetWristInfoFailed,
            LabelAvatarScalingDisabled,
            WaitLoading,
            ButtonLite,
            ButtonStd,
            CheckBoxInstant,
            CheckBoxFixedRate,
            CheckBoxIsMultiplier,
            GroupBoxGesture,
            ComboBoxGesture0,
            ComboBoxGesture1,
            ComboBoxGesture2,
            ComboBoxGesture3,
            ComboBoxGesture4,
            ComboBoxGesture5,
            ComboBoxGesture6,
            ComboBoxGesture7,
            ComboBoxGesture8,
            ComboBoxGesture9,
            CheckBoxGestureMuteDoubleClickMode,
            CheckBoxWorldScaling,
            GroupBoxCustom,
            ButtonCustomImport,
            ButtonCustomExport,
        }
        public string GroupBoxHeight { get; set; } = "groupBoxHeight";
        public string LabelHeightRange { get; set; } = "labelHeightRange";
        public string HeightRangeVRChatAvatar { get; set; } = "heightRangeVRChatAvatar";
        public string HeightRangeVRChatWorld { get; set; } = "heightRangeVRChatWorld";
        public string HeightRangeAdvanced { get; set; } = "heightRangeAdvanced";
        public string HeightRangeLimit { get; set; } = "heightRangeLimit";
        public string HeightRangeUserSettings { get; set; } = "heightRangeUserSettings";
        public string HeightRangeSetUpper { get; set; } = "heightRangeSetUpper";
        public string HeightRangeSetLower { get; set; } = "heightRangeSetLower";
        public string ButtonResetHeight { get; set; } = "buttonResetHeight";
        public string ButtonResetHeightLite { get; set; } = "buttonResetHeightLite";
        public string ButtonChangeScale { get; set; } = "buttonChangeScale";
        public string ButtonStop { get; set; } = "buttonStop";
        public string GroupBoxScalingTime { get; set; } = "groupBoxScalingTime";
        public string GroupBoxScalingRate { get; set; } = "groupBoxScalingRate";
        public string LabelSec { get; set; } = "labelSec";
        public string LabelRate { get; set; } = "labelRate";
        public string LabelMeters { get; set; } = "labelMeters";
        public string LabelMultiplier { get; set; } = "labelMultiplier";
        public string CheckBoxAutoAbort { get; set; } = "checkBoxAutoAbort";
        public string GroupBoxSetting { get; set; } = "groupBoxSetting";
        public string ButtonLanguage { get; set; } = "buttonLanguage";
        public string ButtonFormSize { get; set; } = "buttonFormSize";
        public string GroupBoxOSCConfig { get; set; } = "groupBoxOSCConfig";
        public string OSCStop { get; set; } = "OSCStop";
        public string OSCStart { get; set; } = "OSCStart";
        public string ButtonOSCSetup { get; set; } = "buttonOSCSetup";
        public string LabelOSCIP { get; set; } = "labelOSCIP";
        public string LabelOSCSendPort { get; set; } = "labelOSCSendPort";
        public string LabelOSCReceivePort { get; set; } = "labelOSCReceivePort";
        public string CheckBoxOSCRandomReceiverPort { get; set; } = "checkBoxOSCRandomReceiverPort";
        public string LabelCEH { get; set; } = "labelCEH";
        public string LabelDEH { get; set; } = "labelDEH";
        public string LabelSF { get; set; } = "labelSF";
        public string LabelGetWristInfoFailed { get; set; } = "labelGetWristInfoFailed";
        public string LabelAvatarScalingDisabled { get; set; } = "labelAvatarScalingDisabled";
        public string WaitLoading { get; set; } = "waitLoading";
        public string ButtonLite { get; set; } = "buttonLite";
        public string ButtonStd { get; set; } = "buttonStd";
        public string CheckBoxInstant { get; set; } = "checkBoxInstant";
        public string CheckBoxFixedRate { get; set; } = "checkBoxFixedRate";
        public string CheckBoxIsMultiplier { get; set; } = "checkBoxIsMultiplier";
        public string GroupBoxGesture { get; set; } = "groupBoxGesture";
        public string ComboBoxGesture0 { get; set; } = "comboBoxGesture0";
        public string ComboBoxGesture1 { get; set; } = "comboBoxGesture1";
        public string ComboBoxGesture2 { get; set; } = "comboBoxGesture2";
        public string ComboBoxGesture3 { get; set; } = "comboBoxGesture3";
        public string ComboBoxGesture4 { get; set; } = "comboBoxGesture4";
        public string ComboBoxGesture5 { get; set; } = "comboBoxGesture5";
        public string ComboBoxGesture6 { get; set; } = "comboBoxGesture6";
        public string ComboBoxGesture7 { get; set; } = "comboBoxGesture7";
        public string ComboBoxGesture8 { get; set; } = "comboBoxGesture8";
        public string ComboBoxGesture9 { get; set; } = "comboBoxGesture9";
        public string CheckBoxGestureMuteDoubleClickMode { get; set; } = "checkBoxGestureMuteDoubleClickMode";
        public string CheckBoxWorldScaling { get; set; } = "checkBoxWorldScaling";
        public string GroupBoxCustom { get; set; } = "groupBoxCustom";
        public string ButtonCustomImport { get; set; } = "buttonCustomImport";
        public string ButtonCustomExport { get; set; } = "buttonCustomExport";
        public Localization()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (Enum.TryParse<Key>(prop.Name, out Key result))
                {
                    prop.SetValue(this, Text((Key)result));
                }
            }
        }
        protected virtual string Text(Key text)
        {
            return text switch
            {
                _ => text.ToString(),
            };
        }
    }
}
