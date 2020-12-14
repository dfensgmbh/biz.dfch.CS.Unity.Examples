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
using Assets.Generators;
using Assets.Models;
using UnityEngine;

namespace Assets.Scripts
{
    public class CubeBehaviour : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        private const string TextMeshChildGameObjectName = "TextMesh Child";

        private CubeGenerator cubeGenerator;
        private CubeInfo cubeInfo;

        public double Temperature { get; set; } = 330;
        public TemperatureUnit TemperatureUnit { get; set; } = TemperatureUnit.Kelvin;
        public double EnergyPerMonth { get; set; } = 125d;
        public EnergyUnit EnergyUnit { get; set; } = EnergyUnit.KiloWatt;
        public double SolarPanelSizeInSquareMeter { get; set; } = 1;

        private void Start()
        {
            var childGameObject = new GameObject(TextMeshChildGameObjectName);
            childGameObject.transform.parent = gameObject.transform;
            childGameObject.transform.position = gameObject.transform.position;
            childGameObject.AddComponent<TextMesh>();

            cubeInfo = new CubeInfo(Temperature, TemperatureUnit, EnergyPerMonth, EnergyUnit, SolarPanelSizeInSquareMeter);
            
            cubeGenerator = new CubeGenerator(cubeInfo, gameObject);
            cubeGenerator.Generate();
        }
    }
}
