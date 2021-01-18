/**
 * Copyright 2020 d-fens GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Assets.Constants;
using Assets.Converters;
using Assets.Models;
using UnityEngine;

namespace Assets.Formatters
{
    public class CubeFormatter
    {
        // DFTODO - Some log messages can be put in a resource file
        private const string MainColorName = "_Color";

        private readonly Renderer renderer;
        private readonly TextMesh textMesh;
        private readonly MeshFilter meshFilter;
        private readonly BoxCollider boxCollider;
        private readonly TemperatureConverter temperatureConverter;
        private readonly CubeInfo cubeInfo;

        public CubeFormatter(CubeInfo cubeInfo, GameObject gameObject)
        {
            Contract.Assert(null != cubeInfo);
            Contract.Assert(null != gameObject);

            this.cubeInfo = cubeInfo;
            temperatureConverter = new TemperatureConverter();

            renderer = gameObject.GetComponent<Renderer>();
            textMesh = gameObject.GetComponentInChildren<TextMesh>();
            meshFilter = gameObject.GetComponent<MeshFilter>();
            boxCollider = gameObject.GetComponent<BoxCollider>();

            Contract.Assert(null != renderer);
            Contract.Assert(null != textMesh);
            Contract.Assert(null != meshFilter);
            Contract.Assert(null != boxCollider);
        }

        public bool Format()
        {
            try
            {
                var resultColor = MapTemperatureToColor();
                if (default == resultColor)
                {
                    return false;
                }
                renderer.material.SetColor(MainColorName, resultColor);

                var resultScaleValue = MapEnergyToScaleValue();

                var resultRecalculation = RecalculateCubeBounds(resultScaleValue);
                if (default == resultRecalculation)
                {
                    return false;
                }

                RecalculateBoxColliderSize(resultScaleValue);

                var isDisplayed = DisplayInfoOnCube(resultScaleValue);
                if (default == isDisplayed)
                {
                    return false;
                }

                return true;
            }
            catch (ArgumentException e)
            {
                Debug.Log(e);
                return false;
            }
        }

        private Color MapTemperatureToColor()
        {
            Debug.Log($"START Mapping temperature ('{cubeInfo.Temperature} {(cubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : cubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to color");

            var temperature = cubeInfo.Temperature;

            if (cubeInfo.TemperatureUnit != TemperatureUnit.Kelvin)
            {
                Debug.Log($"START Converting temperature ('{temperature} {(cubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : cubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}') to Kelvin...");
                temperature = temperatureConverter.ConvertToKelvin(cubeInfo.Temperature, cubeInfo.TemperatureUnit);
                Debug.Log($"END Converting temperature '{temperature} K");
            }

            var valueR = PropertyCalculator.CalculateRedColorValue(temperature);
            var valueB = 1 - valueR;

            Debug.Log($"Creating color with R '{valueR}' G '0' and B '{valueB}'");

            var result = new Color(valueR, 0, valueB);

            Debug.Log($"END Result Color: '{result}'");

            return result;
        }

        private float MapEnergyToScaleValue()
        {
            Debug.Log($"START Mapping energy ('{cubeInfo.EnergyPerMonth}') to scale value");

            Debug.Log($"Calculating energy per one square meter. Size of solar panel is '{cubeInfo.SolarPanelSizeInSquareMeter}' ");

            var energy = PropertyCalculator.CalculateEnergyPerSquareMeter(cubeInfo.EnergyPerMonth, cubeInfo.SolarPanelSizeInSquareMeter);

            Debug.Log($"Energy per one square meter is: '{energy}'");
            
            var scaleValue = PropertyCalculator.CalculateCubeScaleValue(energy);

            Debug.Log($"End Mapping Energy to scale value ('{scaleValue}')");
            
            return scaleValue; 
        }

        private bool RecalculateCubeBounds(float scaleValue)
        {
            Debug.Log($"START Recalculating bounds for Cube with scale value '{scaleValue}'");

            var mesh = meshFilter.mesh;
            var baseVertices = mesh.vertices;
            if (null == baseVertices)
            {
                Debug.Log("ABORT Recalculating as vertices of Cube mesh is null");
                return false;
            }

            for (var i = 0; i < baseVertices.Length; i++)
            {
                var vertex = baseVertices[i];

                Debug.Log($"Current vertex that gets calculated '{vertex}'");

                vertex = RecalculateVertexWithScaleValue(vertex, scaleValue);

                Debug.Log($"Result vertex that got calculated '{vertex}'");

                baseVertices[i] = vertex;
            }
            
            if (!baseVertices.Any())
            {
                Debug.Log("ABORT Recalculating as calculated vertices of Cube mesh is empty");
                return false;
            }

            mesh.vertices = baseVertices;
            mesh.RecalculateBounds();

            Debug.Log("END Recalculating cube bounds");

            return true;
        }

        private void RecalculateBoxColliderSize(float scaleValue)
        {
            Debug.Log($"START Recalculating BoxCollider size with scale value '{scaleValue}'");

            var recalculateVertex = RecalculateVertexWithScaleValue(boxCollider.size, scaleValue);

            Debug.Log($"Result Vector3 for BoxCollider size: '{recalculateVertex}'");

            boxCollider.size = recalculateVertex;

            Debug.Log("END Recalculating BoxCollider size'");
        }

        private Vector3 RecalculateVertexWithScaleValue(Vector3 vertex, float scaleValue)
        {
            var xVector = vertex.x * scaleValue;
            var yVector = vertex.y * scaleValue;
            var zVector = vertex.z * scaleValue;

            return new Vector3(xVector, yVector, zVector);
        }

        private bool DisplayInfoOnCube(float scaleValue)
        {
            Debug.Log($"START Displaying information on cube with scale value '{scaleValue}'");

            var fontSize = PropertyCalculator.CalculateFontSize(scaleValue);

            Debug.Log($"Calculated FontSize '{fontSize}'");
            
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.characterSize = 0.03f;
            textMesh.fontSize = fontSize;
            textMesh.text = $"Temperature: {cubeInfo.Temperature} {(cubeInfo.TemperatureUnit == TemperatureUnit.Kelvin ? "K" : cubeInfo.TemperatureUnit == TemperatureUnit.Celsius ? "°C" : "°F")}";
            textMesh.text += $"\nEnergy: {cubeInfo.EnergyPerMonth} kWh";

            Debug.Log($"END TextMesh:\nAnchor: '{textMesh.anchor}' \nCharacter Size: '{textMesh.characterSize}' \nFont Size: '{textMesh.fontSize}'");

            return true;
        }
    }
}
