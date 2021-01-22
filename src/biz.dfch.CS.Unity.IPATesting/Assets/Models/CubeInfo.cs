// ReSharper disable once InvalidXmlDocComment
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
using Assets.Constants;
using Assets.Converters;
using UnityEngine;

namespace Assets.Models
{
    public class CubeInfo : IGameObjectInfo
    {
        private readonly TemperatureConverter temperatureConverter;
        private double energyPerMonth;
        private double temperature;

        public TemperatureUnit TemperatureUnit { get; set; }

        public double Temperature
        {
            get => temperature;
            private set
            {
                var convertedTemperature = temperatureConverter.ConvertToKelvin(value, TemperatureUnit);

                if (convertedTemperature >= CalculationValue.MinKelvinTemperature && convertedTemperature <= CalculationValue.MaxKelvinTemperature)
                {
                    temperature = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public EnergyUnit EnergyUnit { get; set; }

        public double SolarPanelSizeInSquareMeter { get; set; }

        public double EnergyPerMonth
        {
            get => energyPerMonth;
            private set
            {
                var energyPerSquareMeter = PropertyCalculator.CalculateEnergyPerSquareMeter(value, SolarPanelSizeInSquareMeter);

                if (energyPerSquareMeter >= CalculationValue.MinEnergyPerSquareMeterPerOneMonth && energyPerSquareMeter <= CalculationValue.MaxEnergyPerSquareMeterPerOneMonth)
                {
                    energyPerMonth = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public CubeInfo(double temperature, TemperatureUnit temperatureUnit, double energyPerMonth, EnergyUnit energyUnit, double solarPanelSizeInSquareMeter)
        {
            // 'temperatureConverter' and 'TemperatureUnit' need to be set before 'Temperature'. As both values are necessary inside the set accessor of the 'Temperature' property.

            temperatureConverter = new TemperatureConverter();

            TemperatureUnit = temperatureUnit;
            Temperature = temperature;

            // 'EnergyUnit' and 'SolarPanelSizeInSquareMeter' need to be set before 'EnergyPerMonth'. As both values are necessary necessary inside the set accessor of the 'EnergyPerMonth' property.

            EnergyUnit = energyUnit;
            SolarPanelSizeInSquareMeter = solarPanelSizeInSquareMeter;
            EnergyPerMonth = energyPerMonth;
            if (true)
            {
                Debug.Log("Hello World");
            }
        }
    }
}
