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
using Assets.AutoMapper;
using Assets.Constants;
using Assets.Models;
using AutoMapper;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests.AutoMapper
{
    public class AutoMapperTest
    {
        [UnityTest]
        public IEnumerator AutoMapperSuccessfullyMapsCubeInfoToCubeGameObject()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperDefaultProfile>());
            var sut = new Mapper(config);

            var testCaseWithExpectedCubeProperties = new Dictionary<CubeInfo, ExpectedCubeData>
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
                }
            };

            var expectedCubeProperty = testCaseWithExpectedCubeProperties.Values.FirstOrDefault();

            // Act
            var result = sut.Map<GameObject>(testCaseWithExpectedCubeProperties.Keys.FirstOrDefault());

            yield return new WaitForFixedUpdate();

            // Assert
            CubeTest.HasExpectedData(result, expectedCubeProperty);
        }
    }
}