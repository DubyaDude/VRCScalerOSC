using System.Globalization;

namespace VRCScalerOSC.Model
{
    public class PoseData(float pX, float pY, float pZ, float rX, float rY, float rZ)
    {
        public float[] Data { get; } = [pX, pY, pZ, rX, rY, rZ];
        public float PX
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
        public float PY
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
        public float PZ
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
        public float RX
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
        public float RY
        {
            get
            {
                return Data[4];
            }
            set
            {
                Data[4] = value;
            }
        }
        public float RZ
        {
            get
            {
                return Data[5];
            }
            set
            {
                Data[5] = value;
            }
        }
        public override string ToString()
        {
            return $"{PX.ToString("0.00", CultureInfo.InvariantCulture)},{PY.ToString("0.00", CultureInfo.InvariantCulture)},{PZ.ToString("0.00", CultureInfo.InvariantCulture)},{RX.ToString("0.00", CultureInfo.InvariantCulture)},{RY.ToString("0.00", CultureInfo.InvariantCulture)},{RZ.ToString("0.00", CultureInfo.InvariantCulture)}";
        }
    }
}
