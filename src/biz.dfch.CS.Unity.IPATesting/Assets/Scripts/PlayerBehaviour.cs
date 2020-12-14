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
        private const string CubeTagName = "Cube";
        // ReSharper disable once InconsistentNaming
        private const string SphereTagName = "Sphere";

        private SceneGenerator sceneGenerator;
        private Rigidbody playerCubeRigidbody;
        private float verticalInput;
        private float horizontalInput;

        private void Start()
        {
            playerCubeRigidbody = GetComponent<Rigidbody>();
            sceneGenerator = new SceneGenerator(this);
        }

        private void Update()
        {
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

            if (collision.gameObject.CompareTag(CubeTagName))
            {
                sceneGenerator.LoadNextScene();
            }
            if (collision.gameObject.CompareTag(SphereTagName))
            {
                sceneGenerator.LoadPreviousScene();
            }
        }
    }
}
