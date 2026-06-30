using System.Globalization;

namespace VRCScalerOSC.Model
{
    public class VRHeadWristData(PoseData head, PoseData leftwrist, PoseData rightwrist)
    {
        public PoseData Head { get; set; } = head;
        public PoseData LeftWrist { get; set; } = leftwrist;
        public PoseData RightWrist { get; set; } = rightwrist;
        public float HandDistance
        {
            get
            {
                return MathF.Pow(MathF.Pow(LeftWrist.PX - RightWrist.PX, 2f) + MathF.Pow(LeftWrist.PY - RightWrist.PY, 2f) + MathF.Pow(LeftWrist.PZ - RightWrist.PZ, 2f), 0.5f);
            }
        }

        public override string ToString()
        {
            return $"{Head}:{LeftWrist}:{RightWrist}:{HandDistance.ToString("0.00", CultureInfo.InvariantCulture)}";
        }
    }
}
