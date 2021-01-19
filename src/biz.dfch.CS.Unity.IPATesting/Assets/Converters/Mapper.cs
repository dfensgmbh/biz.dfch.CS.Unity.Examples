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
    public class Mapper
    {
        private readonly IMapper iMapper;

        public Mapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CsvData, CubeInfo>();
            });

            iMapper = mapperConfiguration.CreateMapper();
        }
        public CubeInfo MapCsvDataToCubeInfo(CsvData source)
        {
            var destination = iMapper.Map<CsvData, CubeInfo>(source);

            return destination;
        }

        public GameObject MapCubeInfoToCube(CubeInfo source)
        {
            var destination = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cubeBehaviour = destination.AddComponent<CubeBehaviour>();

            cubeBehaviour.TemperatureUnit = source.TemperatureUnit;
            cubeBehaviour.Temperature = source.Temperature;
            cubeBehaviour.EnergyUnit = source.EnergyUnit;
            cubeBehaviour.SolarPanelSizeInSquareMeter = source.SolarPanelSizeInSquareMeter;
            cubeBehaviour.EnergyPerMonth = source.EnergyPerMonth;

            return destination;
        }
    }
}
