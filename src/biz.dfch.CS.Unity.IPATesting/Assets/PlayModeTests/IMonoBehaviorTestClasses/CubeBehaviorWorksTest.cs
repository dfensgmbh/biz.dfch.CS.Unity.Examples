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

using Assets.Constants;
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

        private void Awake()
        {
            controller = gameObject.AddComponent<CubeBehaviour>();

            controller.Renderer = gameObject.AddComponent<MeshRenderer>();
            controller.TemperatureUnit = TemperatureUnit.Celsius;
            controller.Temperature = 20d;
            controller.EnergyPerMonth = 75d;
            controller.EnergyUnit = EnergyUnit.KiloWatt;
        }

        private void Update()
        {
            frameCount++;

            // Color Tests

            var expectedColor = new Color(0.747602761f, 0.000f, 0.252397239f, 1.000f);
            var resultColor = controller.Renderer.material.GetColor("_Color");

            Assert.AreEqual(expectedColor.r, resultColor.r);
            Assert.AreEqual(expectedColor.g, resultColor.g);
            Assert.AreEqual(expectedColor.b, resultColor.b);
            Assert.AreEqual(expectedColor.a, resultColor.a);

            // Scale Tests
            
            var expectedVector3 = new Vector3(1, 1, 1);
            var resultVector3 = gameObject.transform.localScale;

            Assert.AreEqual(expectedVector3.y, resultVector3.y);
            Assert.AreEqual(expectedVector3.x, resultVector3.x);
            Assert.AreEqual(expectedVector3.z, resultVector3.z);
        }
        public bool IsTestFinished => frameCount > 0;
    }
}
