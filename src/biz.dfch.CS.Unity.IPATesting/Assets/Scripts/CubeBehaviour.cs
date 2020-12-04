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
        private CubeGenerator cubeGenerator;
        private CubeInfo cubeInfo;

        public Renderer Renderer { get; set; }
        public TextMesh TextMesh { get; set; }
        public MeshFilter MeshFilter { get; set; }
        public double Temperature { get; set; } = 330d;
        public TemperatureUnit TemperatureUnit { get; set; } = TemperatureUnit.Kelvin;
        public double EnergyPerMonth { get; set; } = 140d;
        public EnergyUnit EnergyUnit { get; set; } = EnergyUnit.KiloWatt;

        // Start is called before the first frame update
        void Start()
        {
            Renderer = gameObject.GetComponent<Renderer>();
            MeshFilter = gameObject.GetComponent<MeshFilter>();

            var childGameObject = new GameObject("Child");
            childGameObject.transform.parent = gameObject.transform;
            childGameObject.transform.position = gameObject.transform.position;
            TextMesh = childGameObject.AddComponent<TextMesh>();

            cubeInfo = new CubeInfo
            {
                EnergyPerMonth = EnergyPerMonth,
                EnergyUnit = EnergyUnit,
                Temperature = Temperature,
                TemperatureUnit = TemperatureUnit
            };

            cubeGenerator = new CubeGenerator(cubeInfo, gameObject, Renderer, TextMesh, MeshFilter);
            cubeGenerator.Generate();
        }
    }
}
