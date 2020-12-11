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
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Generators
{
    public class SceneGenerator 
    {
        private int activeSceneIndex;

        public void LoadNextScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            //var result = LoadSceneSingle(activeSceneIndex + 1);

            LoadCubeOnScene();
        }

        public void LoadPreviousScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeSceneIndex - 1, LoadSceneMode.Single);

            LoadCubeOnScene();
        }

        private void DisplayLoadingScreen()
        {

        }

        private void LoadCubeOnScene()
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(0, 1, 0);
            cube.AddComponent<CubeBehaviour>();
        }

        private IEnumerator LoadSceneSingle(int buildIndex)
        {
            AsyncOperation _async = new AsyncOperation();
            _async = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);

            while (!_async.isDone)
            {
                Debug.Log("Loading Scene ...");
                yield return null;
            }

            Scene nextScene = SceneManager.GetSceneByBuildIndex(buildIndex);
            if (nextScene.IsValid())
            {
                SceneManager.SetActiveScene(nextScene);
            }

        }
    }
}
