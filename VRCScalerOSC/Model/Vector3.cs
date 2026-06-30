using System.Globalization;

namespace VRCScalerOSC.Model
{
    public class Vector3(float x, float y, float z)
    {
        public float[] Data { get; } = [x, y, z];
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
        public float Z
        {
            get
            {
                return Data[2];
            }
            set
            {
                Data[2] = value;
            }
        }
        public override string ToString()
        {
            return $"{X.ToString(CultureInfo.InvariantCulture)},{Y.ToString(CultureInfo.InvariantCulture)},{Z.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
