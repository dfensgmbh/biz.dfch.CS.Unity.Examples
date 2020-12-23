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

using System.Collections.Generic;
using Assets.Models;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Factories.GameObjects
{
    public class CubeFactory : GameObjectFactory<CubeInfo>
    {
        public override List<GameObject> CreateMany(List<CubeInfo> cubeInfos)
        {
            var gameObjects = new List<GameObject>();
            var cubeStartPosition = new Vector3(-4, 1, 0);

            var cubePosition = cubeStartPosition;

            foreach (var cubeInfo in cubeInfos)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = cubePosition;
                var cubeBehaviour = cube.AddComponent<CubeBehaviour>();

                cubeBehaviour.TemperatureUnit = cubeInfo.TemperatureUnit;
                cubeBehaviour.Temperature = cubeInfo.Temperature;
                cubeBehaviour.EnergyUnit = cubeInfo.EnergyUnit;
                cubeBehaviour.SolarPanelSizeInSquareMeter = cubeInfo.SolarPanelSizeInSquareMeter;
                cubeBehaviour.EnergyPerMonth = cubeInfo.EnergyPerMonth;

                cubePosition.x += 3;

                gameObjects.Add(cube);
            }

            return gameObjects;
        }

        public override GameObject Create(CubeInfo cubeInfo)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cubeBehaviour = cube.AddComponent<CubeBehaviour>();

            cubeBehaviour.TemperatureUnit = cubeInfo.TemperatureUnit;
            cubeBehaviour.Temperature = cubeInfo.Temperature;
            cubeBehaviour.EnergyUnit = cubeInfo.EnergyUnit;
            cubeBehaviour.SolarPanelSizeInSquareMeter = cubeInfo.SolarPanelSizeInSquareMeter;
            cubeBehaviour.EnergyPerMonth = cubeInfo.EnergyPerMonth;

            return cube;
        }
    }
}
