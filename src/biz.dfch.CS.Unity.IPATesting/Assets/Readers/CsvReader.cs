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
using Assets.Constants;
using Assets.Models;

namespace Assets.Readers
{
    public class CsvReader
    {
        private readonly List<CsvData> csvData = new List<CsvData>
        {
            new CsvData(-40, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2),
            new CsvData(30, TemperatureUnit.Celsius, 100, EnergyUnit.KiloWatt, 1),
            new CsvData(-20, TemperatureUnit.Celsius, 50, EnergyUnit.KiloWatt, 1),
            new CsvData(300, TemperatureUnit.Kelvin, 1000, EnergyUnit.KiloWatt, 11),
            new CsvData(5, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2),
            new CsvData(-100, TemperatureUnit.Fahrenheit, 20, EnergyUnit.KiloWatt, 0.25),
            new CsvData(35, TemperatureUnit.Celsius, 300, EnergyUnit.KiloWatt, 2)
        };

        public List<CsvData> GetCsvData()
        {
            return csvData;
        }
    }
}
