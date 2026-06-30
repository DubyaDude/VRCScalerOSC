using System.ComponentModel;
using System.Runtime.CompilerServices;
using VRCScalerOSC.Model;

namespace VRCScalerOSC.ViewModel
{
    public class ViewModel_MCC : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "null")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "null")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name == "null" ? null : name));

        private int _mode = 0;
        public int Mode { get => _mode; set => SetProperty(ref _mode, value); }

        private bool _capture = false;
        public bool Capture { get => _capture; set => SetProperty(ref _capture, value); }

        private bool _captureDelayed = false;
        public bool CaptureDelayed { get => _captureDelayed; set => SetProperty(ref _captureDelayed, value); }

        private bool _triggerTakesPhotos = false;
        public bool TriggerTakesPhotos { get => _triggerTakesPhotos; set => SetProperty(ref _triggerTakesPhotos, value); }

        private bool _dollyPathsStayVisible = false;
        public bool DollyPathsStayVisible { get => _dollyPathsStayVisible; set => SetProperty(ref _dollyPathsStayVisible, value); }

        private bool _rollWhileFlying = false;
        public bool RollWhileFlying { get => _rollWhileFlying; set => SetProperty(ref _rollWhileFlying, value); }

        private bool _greenScreen = false;
        public bool GreenScreen { get => _greenScreen; set => SetProperty(ref _greenScreen, value); }

        private bool _lock = false;
        public bool Lock { get => _lock; set => SetProperty(ref _lock, value); }

        private bool _orientationIsLandscape = false;
        public bool OrientationIsLandscape { get => _orientationIsLandscape; set => SetProperty(ref _orientationIsLandscape, value); }

        private bool _flying = false;
        public bool Flying { get => _flying; set => SetProperty(ref _flying, value); }

        private bool _smoothMovement = false;
        public bool SmoothMovement { get => _smoothMovement; set => SetProperty(ref _smoothMovement, value); }

        private bool _autoLevelRoll = false;
        public bool AutoLevelRoll { get => _autoLevelRoll; set => SetProperty(ref _autoLevelRoll, value); }

        private bool _autoLevelPitch = false;
        public bool AutoLevelPitch { get => _autoLevelPitch; set => SetProperty(ref _autoLevelPitch, value); }

        private bool _showUIInCamera = false;
        public bool ShowUIInCamera { get => _showUIInCamera; set => SetProperty(ref _showUIInCamera, value); }

        private bool _localPlayer = true;
        public bool LocalPlayer { get => _localPlayer; set => SetProperty(ref _localPlayer, value); }

        private bool _remotePlayer = true;
        public bool RemotePlayer { get => _remotePlayer; set => SetProperty(ref _remotePlayer, value); }

        private bool _environment = true;
        public bool Environment { get => _environment; set => SetProperty(ref _environment, value); }

        private bool _streaming = false;
        public bool Streaming { get => _streaming; set => SetProperty(ref _streaming, value); }

        private bool _showFocus = false;
        public bool ShowFocus { get => _showFocus; set => SetProperty(ref _showFocus, value); }

        private bool _audioFromCamera = false;
        public bool AudioFromCamera { get => _audioFromCamera; set => SetProperty(ref _audioFromCamera, value); }

        private bool _lookAtMe = false;
        public bool LookAtMe { get => _lookAtMe; set => SetProperty(ref _lookAtMe, value); }

        private float _zoom = 45f;
        public float Zoom { get => _zoom; set => SetProperty(ref _zoom, value); }

        private float _exposure = 0f;
        public float Exposure { get => _exposure; set => SetProperty(ref _exposure, value); }

        private float _focalDistance = 1.5f;
        public float FocalDistance { get => _focalDistance; set => SetProperty(ref _focalDistance, value); }

        private float _aperture = 15f;
        public float Aperture { get => _aperture; set => SetProperty(ref _aperture, value); }

        private float _hue = 120f;
        public float Hue { get => _hue; set => SetProperty(ref _hue, value); }

        private float _saturation = 100f;
        public float Saturation { get => _saturation; set => SetProperty(ref _saturation, value); }

        private float _lightness = 60f;
        public float Lightness { get => _lightness; set => SetProperty(ref _lightness, value); }

        private float _flySpeed = 3f;
        public float FlySpeed { get => _flySpeed; set => SetProperty(ref _flySpeed, value); }

        private float _turnSpeed = 1f;
        public float TurnSpeed { get => _turnSpeed; set => SetProperty(ref _turnSpeed, value); }

        private float _smoothingStrength = 5f;
        public float SmoothingStrength { get => _smoothingStrength; set => SetProperty(ref _smoothingStrength, value); }

        private float _photoRate = 1f;
        public float PhotoRate { get => _photoRate; set => SetProperty(ref _photoRate, value); }

        private float _duration = 2f;
        public float Duration { get => _duration; set => SetProperty(ref _duration, value); }

        private float _lookAtMeXOffset = 0f;
        public float LookAtMeXOffset { get => _lookAtMeXOffset; set => SetProperty(ref _lookAtMeXOffset, value); }

        private float _lookAtMeYOffset = 0f;
        public float LookAtMeYOffset { get => _lookAtMeYOffset; set => SetProperty(ref _lookAtMeYOffset, value); }
        private PoseData _pose = new(0f, 0f, 0f, 0f, 0f, 0f);
        public PoseData Pose { get => _pose; set { SetProperty(ref _pose, value); SetProperty(ref _posePX, _pose.PX); SetProperty(ref _posePX, _pose.PY); SetProperty(ref _posePX, _pose.PZ); SetProperty(ref _posePX, _pose.RX); SetProperty(ref _posePX, _pose.RY); SetProperty(ref _posePX, _pose.RZ); } }

        private float _posePX = 0f;
        public float PosePX { get => _pose.PX; set { _pose.PX = value; _posePX = value; SetProperty(ref _pose, _pose); SetProperty(ref _posePX, value); } }

        private float _posePY = 0f;
        public float PosePY { get => _pose.PY; set { _pose.PY = value; _posePY = value; SetProperty(ref _pose, _pose); SetProperty(ref _posePY, value); } }

        private float _posePZ = 0f;
        public float PosePZ { get => _pose.PZ; set { _pose.PZ = value; _posePZ = value; SetProperty(ref _pose, _pose); SetProperty(ref _posePZ, value); } }

        private float _poseRX = 0f;
        public float PoseRX { get => _pose.RX; set { _pose.RX = value; _poseRX = value; SetProperty(ref _pose, _pose); SetProperty(ref _poseRX, value); } }

        private float _poseRY = 0f;
        public float PoseRY { get => _pose.RY; set { _pose.RY = value; _poseRY = value; SetProperty(ref _pose, _pose); SetProperty(ref _poseRY, value); } }

        private float _poseRZ = 0f;
        public float PoseRZ { get => _pose.RZ; set { _pose.RZ = value; _poseRZ = value; SetProperty(ref _pose, _pose); SetProperty(ref _poseRZ, value); } }

        private PoseData _ctrlPose = new(0f, 0f, 0f, 0f, 0f, 0f);
        public PoseData CtrlPose { get => _ctrlPose; set => SetProperty(ref _ctrlPose, value); }

        private float _ctrlPosePX = 0f;
        public float CtrlPosePX { get => _ctrlPose.PX; set { _ctrlPose.PX = value; _ctrlPosePX = value; SetProperty(ref _ctrlPose, _ctrlPose); SetProperty(ref _ctrlPosePX, value); } }

        private float _ctrlPosePY = 0f;
        public float CtrlPosePY { get => _ctrlPose.PY; set { _ctrlPose.PY = value; _ctrlPosePY = value; SetProperty(ref _ctrlPose, _ctrlPose); SetProperty(ref _ctrlPosePY, value); } }

        private float _ctrlPosePZ = 0f;
        public float CtrlPosePZ { get => _ctrlPose.PZ; set { _ctrlPose.PZ = value; _ctrlPosePZ = value; SetProperty(ref _ctrlPose, _ctrlPose); SetProperty(ref _ctrlPosePZ, value); } }

        private float _ctrlPoseRX = 0f;
        public float CtrlPoseRX { get => _ctrlPose.RX; set { _ctrlPose.RX = value; _ctrlPoseRX = value; SetProperty(ref _ctrlPose, _ctrlPose); SetProperty(ref _ctrlPoseRX, value); } }

        private float _ctrlPoseRY = 0f;
        public float CtrlPoseRY { get => _ctrlPose.RY; set { _ctrlPose.RY = value; _ctrlPoseRY = value; SetProperty(ref _ctrlPose, _ctrlPose); SetProperty(ref _ctrlPoseRY, value); } }

        private float _ctrlPoseRZ = 0f;
        public float CtrlPoseRZ { get => _ctrlPose.RZ; set { _ctrlPose.RZ = value; _ctrlPoseRZ = value; SetProperty(ref _ctrlPose, _ctrlPose); SetProperty(ref _ctrlPoseRZ, value); } }

        private float _modifyRate = 1f;

        public float ModifyRate { get => _modifyRate; set => SetProperty(ref _modifyRate, value); }
    }
}