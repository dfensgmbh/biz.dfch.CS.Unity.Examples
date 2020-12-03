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
            
            // DFTODO - ReSharper Message
            var resultScaleValue = MapEnergyToScaleValue();
            if (default == resultScaleValue)
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

            DisplayInfoOnCube(resultScaleValue);
            
            return true;
        }

        private Color MapTemperatureToColor()
        {
            Debug.Log($"START Mapping temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to color");

            if (CubeInfo.TemperatureUnit != TemperatureUnit.Kelvin)
            {
                // DFTODO - CubeInfo Temperature and TemperatureUnit are overwritten if converted --> currently displaying the converted values instead of initial values. 

                Debug.Log($"START Converting temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to Kelvin...");
                CubeInfo.Temperature = temperatureConverter.ConvertToKelvin(CubeInfo.Temperature, CubeInfo.TemperatureUnit);
                CubeInfo.TemperatureUnit = TemperatureUnit.Kelvin;
                Debug.Log($"END Converting temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}')");
            }

            if (CubeInfo.Temperature > CalculationValue.MaxKelvinTemperature || CubeInfo.Temperature < CalculationValue.MinKelvinTemperature)
            {
                // DFTODO - if over/below max/min value maybe take these instead of returning
                Debug.Log($"Temperature ('{CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') is outside temperature range. Min Temperature: '{CalculationValue.MinKelvinTemperature} K' Max Temperature '{CalculationValue.MaxKelvinTemperature} K'");
                return default;
            }

            Debug.Log($"Difference between max and min temperature value is: '{CalculationValue.TemperatureRangeInKelvin}'");

            var valueR = Calculator.CalculateRedColorValue(CubeInfo.Temperature);
            var valueB = 1 - valueR;

            Debug.Log($"Creating color with R '{valueR}' G '0' and B '{valueB}'");

            var result = new Color(valueR, 0, valueB);

            Debug.Log($"END Result Color: '{result}'");

            return result;
        }

        private float MapEnergyToScaleValue()
        {
            // DFTODO - Add size so can be converted to square meter
            // DFTODO - If energy is 0 scale value is 0 --> default

            Debug.Log($"START Mapping energy ('{CubeInfo.EnergyPerMonth}') to Vector3");

            if (CubeInfo.EnergyPerMonth > CalculationValue.MaxEnergyPerSquareMeterPerOneMonth || CubeInfo.EnergyPerMonth < CalculationValue.MinEnergyPerSquareMeterPerOneMonth)
            {
                return default;
            }

            var scale = Calculator.CalculateCubeScaleValue(CubeInfo.EnergyPerMonth);
            Debug.Log($"End Mapping Energy to scale value ('{scale}')");
            
            return scale;
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

        private void DisplayInfoOnCube(float scale)
        {
            Debug.Log($"START Displaying information on cube with scale value '{scale}'");

            var fontSize = Calculator.CalculateFontSize(scale);

            Debug.Log($"Calculated FontSize '{fontSize}'");

            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.characterSize = 0.03f;
            textMesh.fontSize = fontSize;
            textMesh.text = $"Temperature: {CubeInfo.Temperature} {(CubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : CubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}";
            textMesh.text += $"\nEnergy: {CubeInfo.EnergyPerMonth} kWh";

            Debug.Log($"END TextMesh:\nAnchor: '{textMesh.anchor}' \nCharacter Size: '{textMesh.characterSize}' \nFont Size: '{textMesh.fontSize}'");
        }
    }
}
