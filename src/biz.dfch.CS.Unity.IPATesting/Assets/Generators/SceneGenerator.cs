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

using System.Collections;
using Assets.Constants;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Generators
{
    public class SceneGenerator 
    {
        private int activeSceneIndex;
        private readonly MonoBehaviour monoBehaviour;
        private GameObject cube;

        public SceneGenerator(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour;
            Debug.Log(this.monoBehaviour.GetType());
        }

        public void LoadNextScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Debug.Log($"Active scene buildIndex is '{activeSceneIndex}'");
            
            monoBehaviour.StartCoroutine(LoadScene(activeSceneIndex + 1, activeSceneIndex));
        }

        public void LoadPreviousScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Debug.Log($"Active scene buildIndex is '{activeSceneIndex}'");

            monoBehaviour.StartCoroutine(LoadScene(activeSceneIndex - 1, activeSceneIndex));
        }

        private void CreateCube()
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(0, 1, 0);
            var cubeBehaviour = cube.AddComponent<CubeBehaviour>();

            cubeBehaviour.Temperature = 30;
            cubeBehaviour.TemperatureUnit = TemperatureUnit.Celsius;
            cubeBehaviour.EnergyPerMonth = 100;
            cubeBehaviour.EnergyUnit = EnergyUnit.KiloWatt;
            cubeBehaviour.SolarPanelSizeInSquareMeter = 1;
        }

        private IEnumerator LoadScene(int sceneToLoadBuildIndex, int activeSceneBuildIndex)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneToLoadBuildIndex, LoadSceneMode.Additive);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(activeSceneBuildIndex));

            var scene = SceneManager.GetSceneByBuildIndex(sceneToLoadBuildIndex);
            
            CreateCube();

            SceneManager.MoveGameObjectToScene(cube, scene);
        }
    }
}
