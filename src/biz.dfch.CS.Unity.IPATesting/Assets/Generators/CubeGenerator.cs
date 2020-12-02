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
        // Average gain of solar energy in swiss alps is 135 kWh/m2 during April  
        private const int MaxEnergyPerSquareMeterPerOneMonth = 150;

        // ReSharper disable once InconsistentNaming
        private const int MinEnergyPerSquareMeterPerOneMonth = 0;

        // ReSharper disable once InconsistentNaming
        private const int EnergyRange = MaxEnergyPerSquareMeterPerOneMonth - MinEnergyPerSquareMeterPerOneMonth;

        // ReSharper disable once InconsistentNaming
        private const int MaxVector3CubeValue = 2;

        // ReSharper disable once InconsistentNaming
        private const int MinVector3CubeValue = 0;

        // ReSharper disable once InconsistentNaming
        private const int Vector3CubeValueRange = MaxVector3CubeValue - MinVector3CubeValue;

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
        private readonly TextMesh textMesh;
        private readonly TemperatureConverter temperatureConverter;
        public readonly CubeInfo CubeInfo;
        public readonly GameObject GameObject;

        public CubeGenerator(CubeInfo cubeInfo, GameObject gameObject, Renderer renderer, TextMesh textMesh)
        {
            Contract.Assert(null != cubeInfo);
            Contract.Assert(null != gameObject);
            Contract.Assert(null != renderer);
            Contract.Assert(null != textMesh);

            CubeInfo = cubeInfo;
            GameObject = gameObject;
            this.renderer = renderer;
            this.textMesh = textMesh;

            temperatureConverter = new TemperatureConverter();
        }

        public bool Generate()
        {
            var resultColor = MapTemperatureToColor();
            if (default == resultColor)
            {
                Debug.Log($"Ups, Something went wrong");
                return false;
            }
            renderer.material.SetColor(MainColorName, resultColor);

            var resultVector3 = MapEnergyToVector3();
            if (default== resultVector3)
            {
                Debug.Log($"Ups, Something went wrong");
                return false;
            }
            GameObject.transform.localScale = resultVector3;
            //GameObject.GetComponentInChildren<Transform>().localScale = resultVector3;

            DisplayInfoOnCube();
            
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

        private Vector3 MapEnergyToVector3()
        {
            // DFTODO - Add size so can be converted to square meter

            Debug.Log($"START Mapping energy ('{CubeInfo.EnergyPerMonth}') to Vector3");

            if (CubeInfo.EnergyPerMonth > MaxEnergyPerSquareMeterPerOneMonth || CubeInfo.EnergyPerMonth < MinEnergyPerSquareMeterPerOneMonth)
            {
                return default;
            }

            var valueVector3 = (float) (EnergyRange - (MaxEnergyPerSquareMeterPerOneMonth - CubeInfo.EnergyPerMonth)) / EnergyRange * Vector3CubeValueRange;
            Debug.Log($"Value Vector3 ('{valueVector3}')");

            var result = new Vector3(valueVector3, valueVector3, valueVector3);

            Debug.Log($"END Result Vector3: '{result}'");
            
            return result;
        }

        private void DisplayInfoOnCube()
        {
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.fontSize = 3;
            textMesh.text = $"Temperature: {CubeInfo.Temperature}  {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}";
        }
    }
}
