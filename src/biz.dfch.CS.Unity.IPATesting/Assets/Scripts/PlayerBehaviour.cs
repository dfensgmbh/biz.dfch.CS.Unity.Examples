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

using System.Linq;
using Assets.Constants;
using Assets.Generators;
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

        private SceneGenerator sceneGenerator;
        private Rigidbody playerCubeRigidbody;
        private float verticalInput;
        private float horizontalInput;
        private Vector3 startPosition = new Vector3(0, 0.2f, -4);

        public Scene ActiveScene { get; set; }
        public bool IsStartPositionSet { get; set; }

        private void Start()
        {
            sceneGenerator = new SceneGenerator(this, gameObject);
            
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
                sceneGenerator.LoadNextScene();
            }
            if (collision.gameObject.CompareTag(GameObjectTag.Sphere))
            {
                sceneGenerator.LoadPreviousScene();
            }
        }

    }
}
