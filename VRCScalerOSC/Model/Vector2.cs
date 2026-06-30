using System.Globalization;

namespace VRCScalerOSC.Model
{
    public class Vector2(float x, float y)
    {
        public float[] Data { get; } = [x, y];
        public float X
        {
            get
            {
                return Data[0];
            }
            set
            {
                Data[0] = value;
            }
        }
        public float Y
        {
            get
            {
                return Data[1];
            }
            set
            {
                Data[1] = value;
            }
        }
        public override string ToString()
        {
            return $"{X.ToString(CultureInfo.InvariantCulture)},{Y.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
