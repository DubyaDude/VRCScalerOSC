using System.Diagnostics;

namespace VRCScalerOSC.Model
{
    public class ScaleData
    {
        public float SmoothScalingIterativeTimeInterval
        {
            get { return 1000f / SmoothScalingIterativeTimesPerSecond; }
        }
        public float SmoothScalingIterativeTimesPerSecond { get; }
        public float HeightOrginal { get; }
        public float HeightTarget { get; }
        public float HeightMin { get; }
        public float HeightMax { get; }
        public float ScalingTime { get; }
        public float RealScalingTime
        {
            get
            {
                _realScalingTime ??= _stopwatch?.ElapsedMilliseconds ?? 0;
                return _realScalingTime.Value / 1000f;
            }
            set
            {
                _realScalingTime = value;
            }
        }
        private readonly Stopwatch? _stopwatch;

        private float? _realScalingTime;
        private float _heightNow;
        private float _iterativeRate;
        public bool IsAutoAbort { get; set; } = false;
        public float HeightNow
        {
            get { return _heightNow; }
        }
        public float IterativeRate
        {
            get { return _iterativeRate; }
        }
        public bool AutoAbort { get; }
        public ScaleData(float eyeheightTarget, float eyeheightNow, float scalingTime = 0f, bool autoAbort = true, float iterativeRate = 1f, float smoothScalingIterativeTimesPerSecond = 50f, float eyeheightMin = 0.01f, float eyeheightMax = 10000f)
        {
            HeightMin = eyeheightMin;
            HeightMax = eyeheightMax;
            HeightOrginal = eyeheightNow;
            SmoothScalingIterativeTimesPerSecond = smoothScalingIterativeTimesPerSecond;
            HeightTarget = eyeheightTarget < eyeheightMin ? eyeheightMin : eyeheightTarget > eyeheightMax ? eyeheightMax : eyeheightTarget;
            _heightNow = eyeheightNow < eyeheightMin ? eyeheightMin : eyeheightNow > eyeheightMax ? eyeheightMax : eyeheightNow;
            AutoAbort = scalingTime != 0f && autoAbort;
            _iterativeRate = iterativeRate;
            ScalingTime = scalingTime;
            HeightMin = eyeheightMin;
            HeightMax = eyeheightMax;
            _stopwatch = Stopwatch.StartNew();
        }
        public float NextEyeheight
        {
            get
            {
                if (_heightNow < 0.05f && MathF.Abs(_heightNow - _heightNow * _iterativeRate) < 0.001f)
                {
                    if (_iterativeRate > 1)
                    {
                        _heightNow += 0.001f;
                    }
                    if (_iterativeRate < 1)
                    {
                        _heightNow -= 0.001f;
                    }
                }
                else
                {
                    _heightNow *= _iterativeRate;
                }

                if (_iterativeRate == 1f ||
                    (_iterativeRate > 1f && _heightNow > HeightTarget) ||
                    (_iterativeRate < 1f && _heightNow < HeightTarget) ||
                    MathF.Abs(_heightNow - HeightTarget) / _heightNow < 0.01f)
                {
                    _heightNow = HeightTarget;
                }
                return _heightNow;
            }
        }
        public bool IsFinish
        {
            get
            {
                return MathF.Abs(_heightNow - HeightTarget) / _heightNow < 0.01f;
            }
        }
        public void UpdateIterativeRate(float newIterativeRate)
        {
            _iterativeRate = newIterativeRate;
        }
    }
}
