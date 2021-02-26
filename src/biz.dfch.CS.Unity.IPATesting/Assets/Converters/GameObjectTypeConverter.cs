/**
 * Copyright 2021 d-fens GmbH
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

using Assets.Models;
using Assets.Scripts;
using AutoMapper;
using UnityEngine;

namespace Assets.Converters
{
    public class GameObjectTypeConverter : ITypeConverter<CubeInfo, GameObject>
    {
        public GameObject Convert(CubeInfo source, GameObject destination, ResolutionContext context)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cubeBehaviour = cube.AddComponent<CubeBehaviour>();

            cubeBehaviour.CubeInfo = source;

            return cube;
        }
    }
}
