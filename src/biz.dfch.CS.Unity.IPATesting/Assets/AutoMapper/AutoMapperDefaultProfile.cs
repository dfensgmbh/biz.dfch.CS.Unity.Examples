﻿/**
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

using Assets.Converters;
using Assets.Models;
using AutoMapper;
using UnityEngine;

namespace Assets.AutoMapper
{
    public class AutoMapperDefaultProfile : Profile
    {
        public AutoMapperDefaultProfile()
        {
            CreateMap<CsvData, CubeInfo>(MemberList.None)
                .ConstructUsing(csvData => 
                    new CubeInfo(csvData.HouseTemperature, csvData.TemperatureUnit, csvData.EnergyPerMonth,
                        csvData.EnergyUnit, csvData.SolarPanelSizeInSquareMeter));
                
            CreateMap<CubeInfo, GameObject>(MemberList.None).ConvertUsing<GameObjectTypeConverter>();
        }
    }
}
