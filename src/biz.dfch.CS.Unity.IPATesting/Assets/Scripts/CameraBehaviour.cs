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
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraBehaviour : MonoBehaviour
    {
        private Transform playerTransform;
        private readonly Vector3 cameraOffset = new Vector3(0f, 1.5f, -5f);

        void Start()
        {
            var playerCube = GameObject.FindGameObjectWithTag(GameObjectTag.PlayerCube);
            if (null != playerCube)
            {
                playerTransform = playerCube.transform;
            }
        }
        
        private void LateUpdate()
        {
            if (null == playerTransform)
            {
                var playerCube = GameObject.FindGameObjectWithTag(GameObjectTag.PlayerCube);
                if (null == playerCube)
                {
                    return;
                }
                playerTransform = playerCube.transform;
            }

            transform.position = playerTransform.TransformPoint(cameraOffset);
            transform.LookAt(playerTransform);
        }
    }
}
