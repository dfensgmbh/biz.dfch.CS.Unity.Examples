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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Assets.AutoMapper;
using Assets.Constants;
using Assets.Factories;
using Assets.Models;
using Assets.Readers;
using Assets.Scripts;
using AutoMapper;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Managers
{
    public class CollisionManager
    {
        private readonly GameObject gameObject;
        private readonly MonoBehaviour monoBehaviour;
        private Scene activeScene;
        private PlayerBehaviour playerBehaviour;
        
        public CollisionManager(GameObject gameObject, MonoBehaviour monoBehaviour)
        {
            Contract.Assert(null != gameObject);
            Contract.Assert(null != monoBehaviour);

            this.monoBehaviour = monoBehaviour;
            this.gameObject = gameObject;

            activeScene = SceneManager.GetActiveScene();
        }

        public void ManageCollision(Collision collision)
        {
            if (collision.gameObject.CompareTag(GameObjectTag.Cube))
            {
                var activeSceneIndex = activeScene.buildIndex;

                monoBehaviour.StartCoroutine(LoadLevel(activeSceneIndex + 1));
            }
            if (collision.gameObject.CompareTag(GameObjectTag.Sphere))
            {
                var activeSceneIndex = activeScene.buildIndex;

                monoBehaviour.StartCoroutine(LoadLevel(activeSceneIndex - 1));
            }
        }

        private IEnumerator LoadLevel(int sceneBuildIndex)
        {
            var loadingSceneAsync = SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Single);

            while (!loadingSceneAsync.isDone)
            {
                Debug.Log("Loading Scene ...");
                yield return null;
            }
            activeScene = SceneManager.GetActiveScene();

            var csvReader = new CsvReader();

            var csvData = csvReader.GetCsvData();
            var cubeInfos = new List<CubeInfo>();

            foreach (var data in csvData)
            {
                var config = new MapperConfiguration(cfg =>
                    cfg.AddProfile<AutoMapperDefaultProfile>());
                var mapper = new Mapper(config);
                var cubeInfo = mapper.Map<CubeInfo>(data);

                cubeInfos.Add(cubeInfo);
            }

            var cubeFactory = new CubeFactory();
            var cubes = cubeFactory.CreateMany(cubeInfos);

            foreach (var cube in cubes)
            {
                SceneManager.MoveGameObjectToScene(cube, activeScene);
            }

            playerBehaviour = gameObject.GetComponent<PlayerBehaviour>();
            SetUpBehaviours();
        }

        private void SetUpBehaviours()
        {
            playerBehaviour.ActiveScene = activeScene;
            playerBehaviour.IsStartPositionSet = false;

            var groundGameObject = activeScene.GetRootGameObjects().Single(go => go.CompareTag(GameObjectTag.Ground));
            groundGameObject.AddComponent<GroundBehaviour>();
        }
    }
}
