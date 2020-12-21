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
using System.Collections.Generic;
using System.Linq;
using Assets.Constants;
using Assets.Generators;
using Assets.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PlayerBehaviour : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        private const string Vertical = "Vertical";
        // ReSharper disable once InconsistentNaming
        private const string Horizontal = "Horizontal";
        // ReSharper disable once InconsistentNaming
        private const float MovementSpeed = 4f;
        // ReSharper disable once InconsistentNaming
        private const float RotateSpeed = 75f;
        // ReSharper disable once InconsistentNaming
        private const int YAxisResetHeight = -10;

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

        private CustomSceneManager customSceneManager;
        private GameObjectFactory gameObjectFactory;
        private Rigidbody playerCubeRigidbody;
        private float verticalInput;
        private float horizontalInput;
        private Vector3 startPosition = new Vector3(0, 0.2f, -4);

        public Scene ActiveScene { get; set; }
        public bool IsStartPositionSet { get; set; }

        private void Start()
        {
            customSceneManager = new CustomSceneManager(this);
            gameObjectFactory = new GameObjectFactory();
            
            DontDestroyOnLoad(gameObject);
            
            ActiveScene = SceneManager.GetActiveScene();

            playerCubeRigidbody = GetComponent<Rigidbody>();
            gameObject.tag = GameObjectTag.PlayerCube;
        }

        private void Update()
        {
            if (!IsStartPositionSet)
            {
                var groundGameObject = ActiveScene.GetRootGameObjects().FirstOrDefault(go => go.CompareTag(GameObjectTag.Ground)); 
                if (null != groundGameObject)
                {
                    startPosition.x = groundGameObject.transform.position.x;
                    gameObject.transform.position = startPosition;

                    IsStartPositionSet = true;
                }
            }

            if (gameObject.transform.position.y < YAxisResetHeight)
            {
                gameObject.transform.position = startPosition;
            }

            verticalInput = Input.GetAxis(Vertical) * MovementSpeed;
            horizontalInput = Input.GetAxis(Horizontal) * RotateSpeed;
            Debug.Log(DateTime.Now + " " + SceneManager.GetSceneByBuildIndex(1).isLoaded);
        }

        private void FixedUpdate()
        {
            Vector3 rotation = Vector3.up * horizontalInput;
            Quaternion angleRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);

            playerCubeRigidbody.MovePosition(transform.position + transform.forward * verticalInput * Time.fixedDeltaTime);
            playerCubeRigidbody.MoveRotation(playerCubeRigidbody.rotation * angleRotation);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"Method Call: {nameof(OnCollisionEnter)} Collision Name: {collision.gameObject.name}");

            if (collision.gameObject.CompareTag(GameObjectTag.Cube))
            {
                var scene = customSceneManager.LoadNextScene();
                Debug.Log("Build index after LoadNext" + scene.buildIndex);
                Debug.Log(DateTime.Now + " Testing " + SceneManager.GetSceneByBuildIndex(1).isLoaded);
                var cubes = gameObjectFactory.CreateCubes(cubeInfos);
                Debug.Log(DateTime.Now + " Testing " + SceneManager.GetSceneByBuildIndex(1).isLoaded);
                customSceneManager.AddGameObjectsToScene(cubes, scene);
                Debug.Log(DateTime.Now + " Testing " + SceneManager.GetSceneByBuildIndex(1).isLoaded);
                SetUpBehaviours();
                Debug.Log(DateTime.Now + " Testing " + SceneManager.GetSceneByBuildIndex(1).isLoaded);
                ActiveScene = scene;
                IsStartPositionSet = false;
            }
            if (collision.gameObject.CompareTag(GameObjectTag.Sphere))
            {
                customSceneManager.LoadPreviousScene();
            }
        }

        private void SetUpBehaviours()
        {
            var groundGameObject = ActiveScene.GetRootGameObjects().Single(go => go.CompareTag(GameObjectTag.Ground));
            groundGameObject.AddComponent<GroundBehaviour>();
        }

    }
}
