using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Assets.Calculators;
using Assets.Constants;
using Assets.Converters;
using Assets.Models.Cube;
using UnityEngine;

namespace Assets.Generators
{
    public class CubeGenerator
    {
        // DFTODO - Some log messages can be put in a resource file

        // ReSharper disable once InconsistentNaming
        private const string MainColorName = "_Color";

        private readonly Renderer renderer;
        private readonly TextMesh textMesh;
        private readonly MeshFilter meshFilter;
        private readonly TemperatureConverter temperatureConverter;
        public readonly CubeInfo CubeInfo;
        public readonly GameObject GameObject;

        public CubeGenerator(CubeInfo cubeInfo, GameObject gameObject, Renderer renderer, TextMesh textMesh, MeshFilter meshFilter)
        {
            Contract.Assert(null != cubeInfo);
            Contract.Assert(null != gameObject);
            Contract.Assert(null != renderer);
            Contract.Assert(null != textMesh);
            Contract.Assert(null != meshFilter);

            CubeInfo = cubeInfo;
            GameObject = gameObject;
            this.renderer = renderer;
            this.textMesh = textMesh;
            this.meshFilter = meshFilter;

            temperatureConverter = new TemperatureConverter();
        }

        public bool Generate()
        {
            var resultColor = MapTemperatureToColor();
            if (default == resultColor)
            {
                Debug.Log("Ups, Something went wrong");
                return false;
            }
            renderer.material.SetColor(MainColorName, resultColor);

            float resultScaleValue;
            try
            {
                resultScaleValue = MapEnergyToScaleValue();
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.Log("Ups, Something went wrong");
                return false;
            }

            var resultRecalculation = RecalculateCubeBounds(resultScaleValue);
            if (!resultRecalculation)
            {
                Debug.Log("Ups, Something went wrong");
                return false;
            }

            var isDisplayed = DisplayInfoOnCube(resultScaleValue);
            if (!isDisplayed)
            {
                Debug.Log("Ups, Something went wrong");
                return false;
            }
            
            return true;
        }

        private Color MapTemperatureToColor()
        {
            Debug.Log($"START Mapping temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to color");

            var temperature = CubeInfo.Temperature;

            if (CubeInfo.TemperatureUnit != TemperatureUnit.Kelvin)
            {
                Debug.Log($"START Converting temperature ('{temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to Kelvin...");
                temperature = temperatureConverter.ConvertToKelvin(CubeInfo.Temperature, CubeInfo.TemperatureUnit);
                Debug.Log($"END Converting temperature '{temperature} K");
            }

            try
            {
                var valueR = Calculator.CalculateRedColorValue(temperature);
                var valueB = 1 - valueR;

                Debug.Log($"Creating color with R '{valueR}' G '0' and B '{valueB}'");

                var result = new Color(valueR, 0, valueB);

                Debug.Log($"END Result Color: '{result}'");

                return result;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.Log($"Temperature '{temperature} K' is outside of temperature range. Min Temperature: '{CalculationValue.MinKelvinTemperature} K' Max Temperature '{CalculationValue.MaxKelvinTemperature} K'");
                Debug.Log(e.Message);
                return default;
            }
        }

        private float MapEnergyToScaleValue()
        {
            // DFTODO - Add size so can be converted to square meter
            // DFTODO - If energy is 0 scale value is 0 --> default

            Debug.Log($"START Mapping energy ('{CubeInfo.EnergyPerMonth}') to scale value");
            try
            {
                var scaleValue = Calculator.CalculateCubeScaleValue(CubeInfo.EnergyPerMonth);

                Debug.Log($"End Mapping Energy to scale value ('{scaleValue}')");

                return scaleValue;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.Log($"ABORT Mapping energy '{CubeInfo.EnergyPerMonth}' to scale value as energy is out of energy range. Min energy value: '{CalculationValue.MinEnergyPerSquareMeterPerOneMonth}' Max energy value: '{CalculationValue.MaxEnergyPerSquareMeterPerOneMonth}");
                Debug.Log(e.Message);
                throw;
            }
        }

        private bool RecalculateCubeBounds(float scale)
        {
            Debug.Log($"START Recalculating bounds for Cube with scale value '{scale}'");

            var mesh = meshFilter.mesh;
            var baseVertices = mesh?.vertices;
            if (null == baseVertices)
            {
                Debug.Log("ABORT Recalculating as vertices of Cube mesh is null");
                return false;
            }

            var vertices = new Vector3[baseVertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                var vertex = baseVertices[i];

                Debug.Log($"Current vertex that gets calculated '{vertex}'");

                vertex.x = vertex.x * scale;
                vertex.y = vertex.y * scale;
                vertex.z = vertex.z * scale;

                Debug.Log($"Result vertex that got calculated '{vertex}'");
                Debug.Log($"Selected baseVertices'{baseVertices[i]}'");

                vertices[i] = vertex;
            }

            if (!vertices.Any())
            {
                Debug.Log("ABORT Recalculating as calculated vertices of Cube mesh is empty");
                return false;
            }
            Debug.Log($"Calculated vertices '{vertices}'");

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            Debug.Log("END Recalculating cube bounds");

            return true;
        }

        private bool DisplayInfoOnCube(float scaleValue)
        {
            Debug.Log($"START Displaying information on cube with scale value '{scaleValue}'");

            try
            {
                var fontSize = Calculator.CalculateFontSize(scaleValue);

                Debug.Log($"Calculated FontSize '{fontSize}'");

                textMesh.anchor = TextAnchor.MiddleCenter;
                textMesh.characterSize = 0.03f;
                textMesh.fontSize = fontSize;
                textMesh.text = $"Temperature: {CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}";
                textMesh.text += $"\nEnergy: {CubeInfo.EnergyPerMonth} kWh";

                Debug.Log($"END TextMesh:\nAnchor: '{textMesh.anchor}' \nCharacter Size: '{textMesh.characterSize}' \nFont Size: '{textMesh.fontSize}'");

                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.Log($"ABORT Displaying information on cube as scale value '{scaleValue}' is out of range. Min scale value: '{CalculationValue.MinCubeScaleValue}' Max scale value: '{CalculationValue.MaxCubeScaleValue}'");
                Debug.Log(e.Message);
                return false;
            }
        }
    }
}
