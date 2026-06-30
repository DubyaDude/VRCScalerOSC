using System.Globalization;

namespace VRCScalerOSC.Model
{
    public class Vector4(float x, float y, float z, float w)
    {
        public float[] Data { get; } = [x, y, z, w];
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
        public float W
        {
            get
            {
                return Data[3];
            }
            set
            {
                Data[3] = value;
            }
        }
        public override string ToString()
        {
            return $"{X.ToString(CultureInfo.InvariantCulture)},{Y.ToString(CultureInfo.InvariantCulture)},{Z.ToString(CultureInfo.InvariantCulture)},{W.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
