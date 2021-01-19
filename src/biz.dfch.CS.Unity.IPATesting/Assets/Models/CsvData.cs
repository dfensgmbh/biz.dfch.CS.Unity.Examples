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

using Assets.Constants;

namespace Assets.Models
{
    public class CsvData
    {
        public double Temperature { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
        public double EnergyPerMonth { get; set; }
        public EnergyUnit EnergyUnit { get; set; }
        public double SolarPanelSizeInSquareMeter { get; set; }

        public CsvData(double temperature, TemperatureUnit temperatureUnit, double energyPerMonth, EnergyUnit energyUnit, double solarPanelSizeInSquareMeter)
        {
            Temperature = temperature;
            TemperatureUnit = temperatureUnit;
            EnergyPerMonth = energyPerMonth;
            EnergyUnit = energyUnit;
            SolarPanelSizeInSquareMeter = solarPanelSizeInSquareMeter;
        }
    }
}
