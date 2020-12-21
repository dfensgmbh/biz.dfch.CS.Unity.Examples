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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Generators
{
    public class CustomSceneManager
    {
        private readonly MonoBehaviour monoBehaviour;
        private int activeSceneIndex;
        private Scene activeScene;
        private bool isSceneLoaded;

        public CustomSceneManager(MonoBehaviour monoBehaviour)
        {
            this.monoBehaviour = monoBehaviour;
        }

        public Scene LoadNextScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            Debug.Log($"Active scene buildIndex is '{activeSceneIndex}'");

            monoBehaviour.StartCoroutine(LoadScene2(activeSceneIndex + 1));

           while (!isSceneLoaded)
           {
               Debug.Log("Loading Scene ...");
           }

            //var result = LoadScene(activeSceneIndex + 1);
            Debug.Log("Testing After Coroutine");
            activeScene = SceneManager.GetActiveScene();
            activeSceneIndex = activeScene.buildIndex;

            Debug.Log("Testing After Manger LoadScene");

            return activeScene;
        }

        public Scene LoadPreviousScene()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Debug.Log($"Active scene buildIndex is '{activeSceneIndex}'");

            return activeScene;
        }
        
        private IEnumerator LoadScene2(int sceneToLoadBuildIndex)
        {
            var loadingSceneAsync = SceneManager.LoadSceneAsync(sceneToLoadBuildIndex, LoadSceneMode.Single);

            while (!loadingSceneAsync.isDone)
            {
                Debug.Log("Loading Scene ...");
                yield return null;
            }
            
            activeScene = SceneManager.GetActiveScene();
            activeSceneIndex = activeScene.buildIndex;
            Debug.Log($"Testing After Set active: {activeSceneIndex}");
            isSceneLoaded = true;
        }
        
        private bool LoadScene(int sceneToLoadBuildIndex)
        {
            SceneManager.LoadScene(sceneToLoadBuildIndex, LoadSceneMode.Single);

            return true;
        }

        public void AddGameObjectsToScene(List<GameObject> gameObjects, Scene scene)
        {
            foreach (var gameObject in gameObjects)
            {
                SceneManager.MoveGameObjectToScene(gameObject, scene);
            }
        }
    }
}
