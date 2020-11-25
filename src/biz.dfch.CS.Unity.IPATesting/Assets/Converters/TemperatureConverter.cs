using Assets.Constants;

namespace Assets.Converters
{
    public class TemperatureConverter
    {
        // ReSharper disable once InconsistentNaming
        private const double DeltaKelvinCelsius = 273.15d;

        public double ConvertToKelvin(double temperature, TemperatureUnit temperatureUnit)
        {
            switch (temperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return temperature + DeltaKelvinCelsius;
                case TemperatureUnit.Fahrenheit:
                    return (temperature - 32) * 5 / 9 + DeltaKelvinCelsius;
                default:
                    return temperature;
            }
        }

        public double ConvertToFahrenheit(double temperature, string temperatureUnit)
        {
            return default;
        }       
        
        public double ConvertToCelsius(double temperature, string temperatureUnit)
        {
            return default;
        }
    }
}
