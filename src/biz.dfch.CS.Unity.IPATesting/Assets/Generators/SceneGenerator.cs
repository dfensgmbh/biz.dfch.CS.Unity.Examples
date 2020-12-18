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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Constants;
using Assets.Models;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Generators
{
    public class SceneGenerator 
    {
        private readonly Vector3 cubeStartPosition = new Vector3(-4, 1, 0);
        private readonly MonoBehaviour monoBehaviour;
        private readonly List<CubeInfo> cubeInfos = new List<CubeInfo>()
        {
            new CubeInfo(-40, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2),
            new CubeInfo(30, TemperatureUnit.Celsius, 100, EnergyUnit.KiloWatt, 1),
            new CubeInfo(-20, TemperatureUnit.Celsius, 50, EnergyUnit.KiloWatt, 1),
            new CubeInfo(300, TemperatureUnit.Kelvin, 1000, EnergyUnit.KiloWatt, 11),
            new CubeInfo(5, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2),
            new CubeInfo(-100, TemperatureUnit.Fahrenheit, 20, EnergyUnit.KiloWatt, 0.25),
            new CubeInfo(35, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2)
        };

        private int activeSceneIndex;
        private Scene scene;
        private List<GameObject> rootGameObjects;
        private readonly GameObject playerCube;
        
        public SceneGenerator(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour;

            playerCube = GameObject.FindGameObjectWithTag(GameObjectTag.PlayerCube);
        }

        public SceneGenerator(MonoBehaviour monoBehaviour, GameObject gameObject)
        {
            this.monoBehaviour = monoBehaviour;

            if (gameObject.CompareTag(GameObjectTag.PlayerCube))
            {
                playerCube = gameObject;
            }
            else
            {
                throw new InvalidDataException();
            }
        }

        public void LoadNextScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            Debug.Log($"Active scene buildIndex is '{activeSceneIndex}'");

            monoBehaviour.StartCoroutine(LoadScene(activeSceneIndex + 1));
        }

        public void LoadPreviousScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Debug.Log($"Active scene buildIndex is '{activeSceneIndex}'");

            monoBehaviour.StartCoroutine(LoadScene(activeSceneIndex - 1));
        }

        private void CreateCubesOnScene()
        {
            var cubePosition = cubeStartPosition;

            foreach (var cubeInfo in cubeInfos)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = cubePosition;
                var cubeBehaviour = cube.AddComponent<CubeBehaviour>();
                
                cubeBehaviour.TemperatureUnit = cubeInfo.TemperatureUnit;
                cubeBehaviour.Temperature = cubeInfo.Temperature;
                cubeBehaviour.EnergyUnit = cubeInfo.EnergyUnit;
                cubeBehaviour.SolarPanelSizeInSquareMeter = cubeInfo.SolarPanelSizeInSquareMeter;
                cubeBehaviour.EnergyPerMonth = cubeInfo.EnergyPerMonth;

                cubePosition.x += 3;
            }
        }

        private IEnumerator LoadScene(int sceneToLoadBuildIndex)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneToLoadBuildIndex, LoadSceneMode.Single);

            while (!loadSceneAsync.isDone)
            {
                yield return null;
            }

            CreateCubesOnScene();

            scene = SceneManager.GetActiveScene();
            rootGameObjects = scene.GetRootGameObjects().ToList();

            SetUpBehaviours();
        }

        private void SetUpBehaviours()
        {
            var groundGameObject = rootGameObjects.Single(go => go.name == GameObjectTag.Ground);
            var groundBehaviour = groundGameObject.AddComponent<GroundBehaviour>();
            groundBehaviour.ActiveScene = scene;

            var playerBehaviour = playerCube.GetComponent<PlayerBehaviour>();
            playerBehaviour.ActiveScene = scene;
            playerBehaviour.IsStartPositionSet = false;
        }
    }
}
