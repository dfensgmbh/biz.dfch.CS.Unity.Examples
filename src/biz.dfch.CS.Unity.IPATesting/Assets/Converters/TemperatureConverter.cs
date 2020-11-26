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

        public double ConvertToFahrenheit(double temperature, TemperatureUnit temperatureUnit)
        {
            switch (temperatureUnit)
            {
                case TemperatureUnit.Celsius:
                    return temperature * 9 / 5  + 32;
                case TemperatureUnit.Kelvin:
                    return (temperature - DeltaKelvinCelsius) * 9 / 5 + 32;
                default:
                    return temperature;
            }
        }       
        
        public double ConvertToCelsius(double temperature, TemperatureUnit temperatureUnit)
        {
            switch (temperatureUnit)
            {
                case TemperatureUnit.Kelvin:
                    return temperature - DeltaKelvinCelsius;
                case TemperatureUnit.Fahrenheit:
                    return (temperature - 32) * 5 / 9;
                default:
                    return temperature;
            }
        }
    }
}
