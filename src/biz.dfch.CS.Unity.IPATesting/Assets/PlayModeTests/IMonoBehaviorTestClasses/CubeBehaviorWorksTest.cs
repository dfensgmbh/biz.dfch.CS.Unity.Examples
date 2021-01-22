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
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests.IMonoBehaviorTestClasses
{
    public class CubeBehaviorWorksTest : MonoBehaviour, IMonoBehaviourTest
    {
        private int frameCount;
        private CubeBehaviour cubeBehaviour;
        private GameObject cube;
        private readonly Vector3 baseBoxColliderSize = new Vector3(1, 1, 1);

        public bool IsTestFinished => frameCount > cubeTestInfo.TestCaseWithExpectedCubeProperties.Count - 1;

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

        private readonly CubeTestInfo cubeTestInfo = new CubeTestInfo
        {
            TestCaseWithExpectedCubeProperties = new Dictionary<CubeInfo, ExpectedCubeData>
            {
                {
                    new CubeInfo(20d, TemperatureUnit.Celsius, 75d, EnergyUnit.KiloWatt, 8),
                    new ExpectedCubeData
                    {
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
                    }
                },
                {
                    new CubeInfo(330d, TemperatureUnit.Kelvin, 75, EnergyUnit.KiloWatt, 0.5),
                    new ExpectedCubeData
                    {
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
                }
            }
        };

        private void Awake()
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cubeBehaviour = cube.AddComponent<CubeBehaviour>();

            cubeBehaviour.TemperatureUnit = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.First().TemperatureUnit;
            cubeBehaviour.Temperature = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.First().Temperature;
            cubeBehaviour.EnergyPerMonth = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.First().EnergyPerMonth;
            cubeBehaviour.SolarPanelSizeInSquareMeter = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.First().SolarPanelSizeInSquareMeter;
            cubeBehaviour.EnergyUnit = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.First().EnergyUnit;

            Debug.Log($"TestCases Count: {cubeTestInfo.TestCaseWithExpectedCubeProperties.Count}");
        }

        private void Update()
        {
            Debug.Log($"Frame Count: {frameCount}");

            var testCase = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.ToList()[frameCount];
            var expectedValues = cubeTestInfo.TestCaseWithExpectedCubeProperties.Values.ToList()[frameCount];

            Debug.Log($"Temperature: {testCase.Temperature}");
            Debug.Log($"TemperatureUnit: {testCase.TemperatureUnit}");
            Debug.Log($"EnergyPerMonth: {testCase.EnergyPerMonth}");
            Debug.Log($"EnergyUnit: {testCase.EnergyUnit}");
            Debug.Log($"SolarPanelSizeInSquareMeter: {testCase.SolarPanelSizeInSquareMeter}");

            CubeTest.HasExpectedData(cube, expectedValues);

            frameCount++;

            ResetCubeSize();
            DestroyComponents();
            LoadNextTestCase();
        }

        private void LoadNextTestCase()
        {
            if (frameCount > cubeTestInfo.TestCaseWithExpectedCubeProperties.Count - 1) return;

            var testCase = cubeTestInfo.TestCaseWithExpectedCubeProperties.Keys.ToList()[frameCount];

            cubeBehaviour = cube.AddComponent<CubeBehaviour>();
            
            cubeBehaviour.TemperatureUnit = testCase.TemperatureUnit;
            cubeBehaviour.Temperature = testCase.Temperature;
            cubeBehaviour.EnergyPerMonth = testCase.EnergyPerMonth;
            cubeBehaviour.EnergyUnit = testCase.EnergyUnit;
            cubeBehaviour.SolarPanelSizeInSquareMeter = testCase.SolarPanelSizeInSquareMeter;

            Debug.Log("TestCase loaded");
        }

        private void ResetCubeSize()
        {
            var meshFilter = cube.GetComponent<MeshFilter>();
            var boxCollider = cube.GetComponent<BoxCollider>();

            meshFilter.mesh.vertices = baseVerticesForCube;
            meshFilter.mesh.RecalculateBounds();

            boxCollider.size = baseBoxColliderSize;

            Debug.Log("Resetting Cube size completed");
        }

        private void DestroyComponents()
        {
            Destroy(cubeBehaviour);
            DestroyImmediate(cube.GetComponent<Rigidbody>());
            
            var childGameObject = cube.transform.GetChild(0).gameObject;
            DestroyImmediate(childGameObject);
        
            Debug.Log("Destroying CubeBehaviour component and child GameObject completed");
        }
    }

}
