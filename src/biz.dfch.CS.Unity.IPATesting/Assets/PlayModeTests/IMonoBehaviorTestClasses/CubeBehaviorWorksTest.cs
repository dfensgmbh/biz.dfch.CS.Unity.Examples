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

using System.Collections.Generic;
using System.Linq;
using Assets.Constants;
using Assets.Models;
using Assets.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests.IMonoBehaviorTestClasses
{
    public class CubeBehaviorWorksTest : MonoBehaviour, IMonoBehaviourTest
    {
        // ReSharper disable once InconsistentNaming
        private const string MainColorName = "_Color";

        private int frameCount;
        private CubeBehaviour controller;

        private readonly Vector3 baseBoxColliderSize = new Vector3(1, 1, 1);

        private readonly Vector3[] baseVerticesForCube = 
        {
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f)
        };

        private readonly List<CubeBehaviourTestInfo> testCases = new List<CubeBehaviourTestInfo>
        {
            new CubeBehaviourTestInfo
            {
                TestData = new CubeInfo(20d, TemperatureUnit.Celsius, 75d, EnergyUnit.KiloWatt, 8),
                ExpectedColor = new Color(0.747602761f, 0.000f, 0.252397239f, 1.000f),
                ExpectedVertices = new []
                {
                    new Vector3(0.0625f, -0.0625f, 0.0625f),
                    new Vector3(-0.0625f, -0.0625f, 0.0625f),
                    new Vector3(0.0625f, 0.0625f, 0.0625f),
                    new Vector3(-0.0625f, 0.0625f, 0.0625f),
                    new Vector3(0.0625f, 0.0625f, -0.0625f),
                    new Vector3(-0.0625f, 0.0625f, -0.0625f),
                    new Vector3(0.0625f, -0.0625f, -0.0625f),
                    new Vector3(-0.0625f, -0.0625f, -0.0625f),
                    new Vector3(0.0625f, 0.0625f, 0.0625f),
                    new Vector3(-0.0625f, 0.0625f, 0.0625f),
                    new Vector3(0.0625f, 0.0625f, -0.0625f),
                    new Vector3(-0.0625f, 0.0625f, -0.0625f),
                    new Vector3(0.0625f, -0.0625f, -0.0625f),
                    new Vector3(0.0625f, -0.0625f, 0.0625f),
                    new Vector3(-0.0625f, -0.0625f, 0.0625f),
                    new Vector3(-0.0625f, -0.0625f, -0.0625f),
                    new Vector3(-0.0625f, -0.0625f, 0.0625f),
                    new Vector3(-0.0625f, 0.0625f, 0.0625f),
                    new Vector3(-0.0625f, 0.0625f, -0.0625f),
                    new Vector3(-0.0625f, -0.0625f, -0.0625f),
                    new Vector3(0.0625f, -0.0625f, -0.0625f),
                    new Vector3(0.0625f, 0.0625f, -0.0625f),
                    new Vector3(0.0625f, 0.0625f, 0.0625f),
                    new Vector3(0.0625f, -0.0625f, 0.0625f)
                },
                ExpectedText = "Temperature: 20 °C\nEnergy: 75 kWh",
                ExpectedFontSize = 45, 
                ExpectedBoxColliderSize = new Vector3(0.125f, 0.125f, 0.125f)
            },
            new CubeBehaviourTestInfo
            {
                TestData = new CubeInfo(330d, TemperatureUnit.Kelvin, 75, EnergyUnit.KiloWatt, 0.5),
                ExpectedColor = new Color(1, 0, 0, 1),
                ExpectedVertices = new []
                {
                    new Vector3(1f, -1f, 1f),
                    new Vector3(-1f, -1f, 1f),
                    new Vector3(1f, 1f, 1f),
                    new Vector3(-1f, 1f, 1f),
                    new Vector3(1f, 1f, -1f),
                    new Vector3(-1f, 1f, -1f),
                    new Vector3(1f, -1f, -1f),
                    new Vector3(-1f, -1f, -1f),
                    new Vector3(1f, 1f, 1f),
                    new Vector3(-1f, 1f, 1f),
                    new Vector3(1f, 1f, -1f),
                    new Vector3(-1f, 1f, -1f),
                    new Vector3(1f, -1f, -1f),
                    new Vector3(1f, -1f, 1f),
                    new Vector3(-1f, -1f, 1f),
                    new Vector3(-1f, -1f, -1f),
                    new Vector3(-1f, -1f, 1f),
                    new Vector3(-1f, 1f, 1f),
                    new Vector3(-1f, 1f, -1f),
                    new Vector3(-1f, -1f, -1f),
                    new Vector3(1f, -1f, -1f),
                    new Vector3(1f, 1f, -1f),
                    new Vector3(1f, 1f, 1f),
                    new Vector3(1f, -1f, 1f)
                },
                ExpectedText = "Temperature: 330 K\nEnergy: 75 kWh",
                ExpectedFontSize = 125,
                ExpectedBoxColliderSize = new Vector3(2, 2 , 2)
            }
        };

        private GameObject cube;
        private Renderer cubeRenderer;
        private MeshFilter meshFilter;
        private BoxCollider boxCollider;
        private TextMesh textMesh;

        private void Awake()
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            controller = cube.AddComponent<CubeBehaviour>();

            controller.TemperatureUnit = testCases.First().TestData.TemperatureUnit;
            controller.Temperature = testCases.First().TestData.Temperature;
            controller.EnergyPerMonth = testCases.First().TestData.EnergyPerMonth;
            controller.SolarPanelSizeInSquareMeter = testCases.First().TestData.SolarPanelSizeInSquareMeter;
            controller.EnergyUnit = testCases.First().TestData.EnergyUnit;

            Debug.Log($"TestCases Count: {testCases.Count}");
        }

        private void Update()
        {
            Debug.Log($"Frame Count: {frameCount}");

            var testCase = testCases[frameCount];

            Debug.Log($"Temperature: {testCase.TestData.Temperature}");
            Debug.Log($"TemperatureUnit: {testCase.TestData.TemperatureUnit}");
            Debug.Log($"EnergyPerMonth: {testCase.TestData.EnergyPerMonth}");
            Debug.Log($"EnergyUnit: {testCase.TestData.EnergyUnit}");
            Debug.Log($"SolarPanelSizeInSquareMeter: {testCase.TestData.SolarPanelSizeInSquareMeter}");

            cubeRenderer = cube.GetComponent<Renderer>();
            meshFilter = cube.GetComponent<MeshFilter>();
            boxCollider = cube.GetComponent<BoxCollider>();
            textMesh = cube.GetComponentInChildren<TextMesh>();

            // Color Tests

            var expectedColor = testCase.ExpectedColor;
            var resultColor = cubeRenderer.material.GetColor(MainColorName);

            Debug.Log($"Expected Color: '{expectedColor}'");
            Debug.Log($"Result Color: '{resultColor}'");

            Assert.AreEqual(expectedColor, resultColor);

            // Scale Tests
            
            var expectedVertices = testCase.ExpectedVertices;
            var resultVertices = meshFilter.mesh.vertices;

            for (int i = 0; i < expectedVertices.Length; i++)
            {
                var expectedVertex = expectedVertices[i];
                var vertexToBeAsserted = resultVertices[i];

                Debug.Log($"Expected vertex: '{expectedVertex}'");
                Debug.Log($"Vertex to be asserted: '{vertexToBeAsserted}'");

                Assert.IsNotNull(expectedVertex);
                Assert.IsNotNull(vertexToBeAsserted);

                Assert.AreEqual(expectedVertex, vertexToBeAsserted);
            }

            // BoxCollider Size Tests

            var expectedBoxColliderSize = testCase.ExpectedBoxColliderSize;
            var resultBoxColliderSize = boxCollider.size;

            Assert.AreEqual(expectedBoxColliderSize, resultBoxColliderSize);

            // Text Tests

            Debug.Log("Children Count: " + cube.GetComponentsInChildren<Component>().Length);
            
            var expectedText = testCase.ExpectedText;
            var resultText = textMesh.text;

            Debug.Log($"Expected text: '{expectedText}'");
            Debug.Log($"Result text: '{resultText}'");

            var expectedFontSize = testCase.ExpectedFontSize;
            var resultFontSize = textMesh.fontSize;

            Debug.Log($"Expected font size: '{expectedFontSize}'");
            Debug.Log($"Result font size: '{resultFontSize}'");

            Assert.AreEqual(expectedText, resultText);
            Assert.AreEqual(expectedFontSize, resultFontSize);

            Debug.Log("Test Finished - Loading next Case...");

            frameCount++;

            ResetCubeSize();
            DestroyComponents();
            LoadNextTestCase();
        }

        public bool IsTestFinished => frameCount > testCases.Count - 1;

        private void LoadNextTestCase()
        {
            if (frameCount > testCases.Count - 1) return;

            var testCase = testCases[frameCount];

            controller = cube.AddComponent<CubeBehaviour>();
            
            controller.TemperatureUnit = testCase.TestData.TemperatureUnit;
            controller.Temperature = testCase.TestData.Temperature;
            controller.EnergyPerMonth = testCase.TestData.EnergyPerMonth;
            controller.EnergyUnit = testCase.TestData.EnergyUnit;
            controller.SolarPanelSizeInSquareMeter = testCase.TestData.SolarPanelSizeInSquareMeter;

            Debug.Log("TestCase loaded");
        }

        private void ResetCubeSize()
        {
            meshFilter.mesh.vertices = baseVerticesForCube;
            meshFilter.mesh.RecalculateBounds();

            boxCollider.size = baseBoxColliderSize;

            Debug.Log("Resetting Cube size completed");
        }

        private void DestroyComponents()
        {
            Destroy(controller);
            
            var childGameObject = cube.transform.GetChild(0).gameObject;
            DestroyImmediate(childGameObject);
        
            Debug.Log("Destroying CubeBehaviour component and child GameObject completed");
        }

        private class CubeBehaviourTestInfo
        {
            public CubeInfo TestData { get; set; }
            public Color ExpectedColor { get; set; }
            public Vector3[] ExpectedVertices { get; set; }
            public string ExpectedText { get; set; }
            public int ExpectedFontSize { get; set; }
            public Vector3 ExpectedBoxColliderSize { get; set; }
        }
    }

}
