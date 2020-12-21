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
using System.Text;
using System.Threading.Tasks;
using Assets.Constants;
using Assets.Models;

namespace Assets.Reader
{
    public class CsvReader
    {
        private readonly List<CubeInfo> cubeInfos = new List<CubeInfo>
        {
            new CubeInfo(-40, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2),
            new CubeInfo(30, TemperatureUnit.Celsius, 100, EnergyUnit.KiloWatt, 1),
            new CubeInfo(-20, TemperatureUnit.Celsius, 50, EnergyUnit.KiloWatt, 1),
            new CubeInfo(300, TemperatureUnit.Kelvin, 1000, EnergyUnit.KiloWatt, 11),
            new CubeInfo(5, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2),
            new CubeInfo(-100, TemperatureUnit.Fahrenheit, 20, EnergyUnit.KiloWatt, 0.25),
            new CubeInfo(35, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2)
        };

        public List<CubeInfo> GetCubeInfos()
        {
            return cubeInfos;
        }
    }
}
