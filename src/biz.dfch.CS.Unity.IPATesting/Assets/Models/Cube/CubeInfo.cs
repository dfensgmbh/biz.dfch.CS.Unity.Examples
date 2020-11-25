using Assets.Constants;

namespace Assets.Models.Cube
{
    public class CubeInfo : ICubeInfo
    {
        public double Temperature { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
        public double Energy { get; set; }
        public EnergyUnit EnergyUnit { get; set; }
    }
}
