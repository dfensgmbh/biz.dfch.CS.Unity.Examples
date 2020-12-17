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

using System.Collections.Generic;
using System.Linq;
using Assets.Constants;
using Assets.Generators;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GroundBehaviour : MonoBehaviour
    {
        private GroundGenerator groundGenerator;
        private List<GameObject> cubesOnScene;

        public Scene ActiveScene { get; set; }
        
        void Start()
        {
            cubesOnScene = ActiveScene.GetRootGameObjects().Where(go => go.CompareTag(GameObjectTag.Cube)).ToList();
            
            groundGenerator = new GroundGenerator(cubesOnScene, gameObject);
            var result = groundGenerator.Generate();

            gameObject.tag = GameObjectTag.Ground;
        }
    }
}
