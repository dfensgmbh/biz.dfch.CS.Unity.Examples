using System;
using Assets.Constants;
using Assets.Converters;

namespace Assets.Models
{
    public class CubeInfo
    {
        private readonly TemperatureConverter temperatureConverter;

        private double temperature;
        public double Temperature
        {
            get => temperature;
            private set
            {
                var convertedTemperature = temperatureConverter.ConvertToKelvin(value, TemperatureUnit);

                if (convertedTemperature >= CalculationValue.MinKelvinTemperature && convertedTemperature <= CalculationValue.MaxKelvinTemperature)
                {
                    temperature = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public TemperatureUnit TemperatureUnit { get; set; }

        private double energyPerMonth;
        public double EnergyPerMonth
        {
            get => energyPerMonth;
            private set
            {
                if (value >= CalculationValue.MinEnergyPerSquareMeterPerOneMonth && value <= CalculationValue.MaxEnergyPerSquareMeterPerOneMonth)
                {
                    energyPerMonth = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public EnergyUnit EnergyUnit { get; set; }

        public CubeInfo(double temperature, TemperatureUnit temperatureUnit, double energyPerMonth, EnergyUnit energyUnit)
        {
            // 'temperatureConverter' and 'TemperatureUnit' need to be set before 'Temperature'. As both values are necessary for setting the 'Temperature'.

            temperatureConverter = new TemperatureConverter();

            TemperatureUnit = temperatureUnit;
            Temperature = temperature;

            EnergyUnit = energyUnit;
            EnergyPerMonth = energyPerMonth;
        }
    }
}
