using Assets.Constants;

namespace Assets.Models
{
    public class CubeInfo
    {
        public double Temperature { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
        public double EnergyPerMonth { get; set; }
        public EnergyUnit EnergyUnit { get; set; }
    }
}
