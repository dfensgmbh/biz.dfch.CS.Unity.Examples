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

using UnityEngine;

namespace Assets.Scripts
{
    public class CameraBehaviour : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        private const string PlayerGameObjectName = "Player";

        private Transform playerTransform;
        private readonly Vector3 cameraOffset = new Vector3(0f, 1.5f, -5f);

        void Start()
        {
            playerTransform = GameObject.Find(PlayerGameObjectName).transform;
        }
        
        private void LateUpdate()
        {
            if (null == playerTransform)
            {
                playerTransform = GameObject.Find(PlayerGameObjectName).transform;
            }

            transform.position = playerTransform.TransformPoint(cameraOffset);
            transform.LookAt(playerTransform);
        }
    }
}
