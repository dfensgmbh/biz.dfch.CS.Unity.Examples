
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
using Assets.Formatters;
using Assets.Models;
using UnityEngine;

namespace Assets.Scripts
{
    public class CubeBehaviour : MonoBehaviour
    {
        private const string TextMeshChildGameObjectName = "TextMesh Child";
        
        private CubeFormatter cubeFormatter;
        private CubeInfo cubeInfo;
        private GameObject childGameObject;

        public double Temperature { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; } 
        public double EnergyPerMonth { get; set; }
        public EnergyUnit EnergyUnit { get; set; } 
        public double SolarPanelSizeInSquareMeter { get; set; }

        private void Start()
        {
            childGameObject = new GameObject(TextMeshChildGameObjectName);
            childGameObject.transform.parent = gameObject.transform;
            childGameObject.transform.position = gameObject.transform.position;
            childGameObject.AddComponent<TextMesh>();

            var cubeRigidbody = gameObject.AddComponent<Rigidbody>();
            cubeRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            //cubeRigidbody.constraints = (RigidbodyConstraints) 122;
            Debug.Log("RigidbodyConstraints are: " + cubeRigidbody.constraints);

            cubeInfo = new CubeInfo(Temperature, TemperatureUnit, EnergyPerMonth, EnergyUnit, SolarPanelSizeInSquareMeter);
            
            cubeFormatter = new CubeFormatter(cubeInfo, gameObject);
            cubeFormatter.Format();

            gameObject.tag = GameObjectTag.Cube;
        }
    }
}
