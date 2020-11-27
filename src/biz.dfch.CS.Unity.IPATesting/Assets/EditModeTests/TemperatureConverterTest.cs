using Assets.Constants;
using Assets.Converters;
using NUnit.Framework;

namespace Assets.EditModeTests
{
    public class TemperatureConverterTest
    {
        [TestCase(50d, TemperatureUnit.Fahrenheit, 283.15d)]
        [TestCase(20d, TemperatureUnit.Celsius, 293.15d)]
        public void ConvertToKelvinReturnsExpectedTemperature(double temperature, TemperatureUnit temperatureUnit, double expectedTemperature)
        {
            // Arrange
            var sut = new TemperatureConverter();

            // Act
            var result = sut.ConvertToKelvin(temperature, temperatureUnit);

            // Assert
            Assert.AreEqual(expectedTemperature, result);
        }

        [TestCase(20d, TemperatureUnit.Celsius, 68d)]
        [TestCase(283.15d, TemperatureUnit.Kelvin, 50d)]
        public void ConvertToFahrenheitReturnsExpectedTemperature(double temperature, TemperatureUnit temperatureUnit, double expectedTemperature)
        {
            // Arrange
            var sut = new TemperatureConverter();

            // Act
            var result = sut.ConvertToFahrenheit(temperature, temperatureUnit);
            
            // Assert
            Assert.AreEqual(expectedTemperature, result);
        }

        [TestCase(77d, TemperatureUnit.Fahrenheit, 25d)]
        [TestCase(288.15d, TemperatureUnit.Kelvin, 15d)]
        public void ConvertToCelsiusReturnsExpectedTemperature(double temperature, TemperatureUnit temperatureUnit, double expectedTemperature)
        {
            // Arrange
            var sut = new TemperatureConverter();

            // Act
            var result = sut.ConvertToCelsius(temperature, temperatureUnit);

            // Assert
            Assert.AreEqual(expectedTemperature, result);
        }
    }
}
