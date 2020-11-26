using Assets.Constants;
using Assets.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace biz.dfch.CS.Unity.IPATesting.Tests.Assets.Converters
{
    [TestClass]
    public class TemperatureConverterTest
    {
        [TestMethod]
        public void ConvertingFahrenheitToKelvinReturnsExpectedResult()
        {
            // Arrange
            var sut = new TemperatureConverter();

            var temperature = 50d;
            var temperatureUnit = TemperatureUnit.Fahrenheit;

            var expectedResult = 283.15d;

            // Act
            var result = sut.ConvertToKelvin(temperature, temperatureUnit);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ConvertingCelsiusToKelvinReturnsExpectedResult()
        {
            // Arrange
            var sut = new TemperatureConverter();

            var temperature = 20d;
            var temperatureUnit = TemperatureUnit.Celsius;

            var expectedResult = 293.15;

            // Act
            var result = sut.ConvertToKelvin(temperature, temperatureUnit);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
