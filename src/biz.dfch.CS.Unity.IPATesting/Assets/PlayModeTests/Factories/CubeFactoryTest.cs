/**
 * Copyright 2021 d-fens GmbH
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Constants;
using Assets.Factories;
using Assets.Models;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests.Factories
{
    public class CubeFactoryTest
    {
        private readonly IDictionary<CubeInfo, ExpectedCubeData> testCaseWithExpectedCubeProperties = new Dictionary<CubeInfo, ExpectedCubeData>
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
        };
            
        [UnityTest]
        public IEnumerator CubeFactoryCreatesOneCubeSuccessfully()
        {
            // Arrange
            var sut = new CubeFactory();
            var expectedCubeProperty = testCaseWithExpectedCubeProperties.Values.First();
            var cubeInfo = testCaseWithExpectedCubeProperties.Keys.First();


            // Act 
            var createdGameObject = sut.Create(cubeInfo);

            yield return new WaitForFixedUpdate();

            // Assert
            CubeTest.HasExpectedData(createdGameObject, expectedCubeProperty);
        }
        
        [UnityTest]
        public IEnumerator CubeFactoryCreatesManyCubesSuccessfully()
        {
            // Arrange
            var sut = new CubeFactory();
            var listOfCubeInfos = testCaseWithExpectedCubeProperties.Keys.ToList();

            // Act 
            var createdGameObjects = sut.CreateMany(listOfCubeInfos);

            yield return new WaitForFixedUpdate();

            // Assert
            for (int i = 0; i < testCaseWithExpectedCubeProperties.Count; i++)
            {
                var expectedCubeProperty = testCaseWithExpectedCubeProperties.Values.ToList()[i];

                CubeTest.HasExpectedData(createdGameObjects[i], expectedCubeProperty);
            }
        }
    }
}