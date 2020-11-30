﻿/**
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
using Assets.Models.Cube;
using Assets.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests.IMonoBehaviorTestClasses
{
    public class CubeBehaviorWorksTest : MonoBehaviour, IMonoBehaviourTest
    {
        private int frameCount;
        private CubeBehaviour controller;

        private readonly List<CubeBehaviourTestInfo> testCases = new List<CubeBehaviourTestInfo>
        {
            new CubeBehaviourTestInfo
            {
                CubeInfoTestValues = new CubeInfo
                {
                    Temperature = 20d,
                    TemperatureUnit = TemperatureUnit.Celsius,
                    EnergyPerMonth = 75d,
                    EnergyUnit = EnergyUnit.KiloWatt
                },
                ExpectedColor = new Color(0.747602761f, 0.000f, 0.252397239f, 1.000f),
                ExpectedVector3 = new Vector3(1, 1, 1)
            },
            new CubeBehaviourTestInfo
            {
                CubeInfoTestValues = new CubeInfo
                {
                    Temperature = 330d,
                    TemperatureUnit = TemperatureUnit.Kelvin,
                    EnergyPerMonth = 150d,
                    EnergyUnit = EnergyUnit.KiloWatt
                },
                ExpectedColor = new Color(1, 0, 0, 1),
                ExpectedVector3 = new Vector3(2, 2, 2)
            }
        };

        private void Awake()
        {
            controller = gameObject.AddComponent<CubeBehaviour>();
            controller.Renderer = gameObject.AddComponent<MeshRenderer>();

            controller.TemperatureUnit = testCases.First().CubeInfoTestValues.TemperatureUnit;
            controller.Temperature = testCases.First().CubeInfoTestValues.Temperature;
            controller.EnergyPerMonth = testCases.First().CubeInfoTestValues.EnergyPerMonth;
            controller.EnergyUnit = testCases.First().CubeInfoTestValues.EnergyUnit;
        }

        private void Update()
        {
            Debug.Log($"Frame Count: {frameCount}");
            Debug.Log($"TestCases Count: {testCases.Count}");

            var testCase = testCases[frameCount];

            Debug.Log("TestCase loaded");
            Debug.Log($"Temperature: {testCase.CubeInfoTestValues.Temperature}");
            Debug.Log($"TemperatureUnit: {testCase.CubeInfoTestValues.TemperatureUnit}");
            Debug.Log($"EnergyPerMonth: {testCase.CubeInfoTestValues.EnergyPerMonth}");
            Debug.Log($"EnergyUnit: {testCase.CubeInfoTestValues.EnergyUnit}");

            // Color Tests

            var expectedColor = testCase.ExpectedColor;
            var resultColor = controller.Renderer.material.GetColor("_Color");

            Debug.Log($"Expected Color: {expectedColor}");
            Debug.Log($"Expected Color: {resultColor}");

            Assert.AreEqual(expectedColor.r, resultColor.r);
            Assert.AreEqual(expectedColor.g, resultColor.g);
            Assert.AreEqual(expectedColor.b, resultColor.b);
            Assert.AreEqual(expectedColor.a, resultColor.a);

            // Scale Tests
            
            var expectedVector3 = testCase.ExpectedVector3;
            var resultVector3 = gameObject.transform.localScale;

            Debug.Log($"Expected Vector3: {expectedVector3}");
            Debug.Log($"Result Vector3: {resultVector3}");

            Assert.AreEqual(expectedVector3.y, resultVector3.y);
            Assert.AreEqual(expectedVector3.x, resultVector3.x);
            Assert.AreEqual(expectedVector3.z, resultVector3.z);
            
            frameCount++;
            Destroy(controller);
            LoadNextTestCase();
        }

        public bool IsTestFinished => frameCount > testCases.Count - 1;

        private void LoadNextTestCase()
        {
            if (frameCount > testCases.Count - 1) return;

            var testCase = testCases[frameCount];

            controller = gameObject.AddComponent<CubeBehaviour>();
            
            controller.TemperatureUnit = testCase.CubeInfoTestValues.TemperatureUnit;
            controller.Temperature = testCase.CubeInfoTestValues.Temperature;
            controller.EnergyPerMonth = testCase.CubeInfoTestValues.EnergyPerMonth;
            controller.EnergyUnit = testCase.CubeInfoTestValues.EnergyUnit;
        }

        private class CubeBehaviourTestInfo
        {
            public CubeInfo CubeInfoTestValues { get; set; }
            public Color ExpectedColor { get; set; }
            public Vector3 ExpectedVector3 { get; set; }
        }
    }

}
