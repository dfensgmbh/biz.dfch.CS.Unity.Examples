using System.Diagnostics.Contracts;
using Assets.Constants;
using Assets.Converters;
using Assets.Models.Cube;
using UnityEngine;

namespace Assets.Generators
{
    public class CubeGenerator
    {
        // ReSharper disable once InconsistentNaming
        // Highest measured temperature on earth
        private const int MaxKelvinTemperature = 330;

        // ReSharper disable once InconsistentNaming
        // Lowest measured temperature on earth
        private const int MinKelvinTemperature = 184;

        // ReSharper disable once InconsistentNaming
        private const int TemperatureRangeInKelvin = MaxKelvinTemperature - MinKelvinTemperature;

        // ReSharper disable once InconsistentNaming
        private const string MainColorName = "_Color";

        private readonly Renderer renderer;
        private readonly TemperatureConverter temperatureConverter;
        public readonly CubeInfo CubeInfo;
        public readonly GameObject GameObject;

        public CubeGenerator(CubeInfo cubeInfo, GameObject gameObject, Renderer renderer)
        {
            Contract.Assert(null != cubeInfo);
            Contract.Assert(null != gameObject);
            Contract.Assert(null != renderer);

            CubeInfo = cubeInfo;
            GameObject = gameObject;
            this.renderer = renderer;

            temperatureConverter = new TemperatureConverter();
        }

        public bool Generate()
        {
            var resultColor = MapTemperatureToColor();
            if (default == resultColor)
            {
                return false;
            }

            renderer.material.SetColor(MainColorName, resultColor);

            return true;
        }

        private Color MapTemperatureToColor()
        {
            Debug.Log($"START Mapping temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to color");

            if (CubeInfo.TemperatureUnit != TemperatureUnit.Kelvin)
            {
                Debug.Log($"START Converting temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to Kelvin...");
                CubeInfo.Temperature = temperatureConverter.ConvertToKelvin(CubeInfo.Temperature, CubeInfo.TemperatureUnit);
                CubeInfo.TemperatureUnit = TemperatureUnit.Kelvin;
                Debug.Log($"END Converting temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}')");
            }

            if (CubeInfo.Temperature > MaxKelvinTemperature || CubeInfo.Temperature < MinKelvinTemperature)
            {
                Debug.Log($"Temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') is outside temperature range. Min Temperature: '{MinKelvinTemperature} K' Max Temperature '{MaxKelvinTemperature} K'");
                return default;
            }

            Debug.Log($"Difference between max and min temperature value is: '{TemperatureRangeInKelvin}'");

            var valueR = (float)(TemperatureRangeInKelvin - (MaxKelvinTemperature - CubeInfo.Temperature)) / TemperatureRangeInKelvin;
            var valueB = 1 - valueR;

            Debug.Log($"Creating color with R '{valueR}' G '0' and B '{valueB}'");

            var result = new Color(valueR, 0, valueB);

            Debug.Log($"END Result Color: '{result}'");

            return result;
        }

        private void SetCubeSize()
        {

        }

        private void DisplayInfoOnCube()
        {

        }
    }
}
