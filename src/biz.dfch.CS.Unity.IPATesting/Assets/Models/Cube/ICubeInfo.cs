using Assets.Constants;

namespace Assets.Models.Cube
{
    interface ICubeInfo
    {
        double Temperature { get; set; }
        TemperatureUnit TemperatureUnit { get; set; }
        double EnergyPerMonth { get; set; }
        EnergyUnit EnergyUnit { get; set; }
    }
}
