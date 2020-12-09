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

using System;
using Assets.Constants;
using Assets.Converters;

namespace Assets.Models
{
    public class CubeInfo
    {
        private readonly TemperatureConverter temperatureConverter;

        private double temperature;
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

        public TemperatureUnit TemperatureUnit { get; set; }

        private double energyPerMonth;
        public double EnergyPerMonth
        {
            get => energyPerMonth;
            private set
            {
                var energyPerOneSquareMeter = PropertyCalculator.CalculateEnergyPerSquareMeter(value, SolarPanelSizeInSquareMeter);

                if (energyPerOneSquareMeter >= CalculationValue.MinEnergyPerSquareMeterPerOneMonth && energyPerOneSquareMeter <= CalculationValue.MaxEnergyPerSquareMeterPerOneMonth)
                {
                    energyPerMonth = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public EnergyUnit EnergyUnit { get; set; }

        public double SolarPanelSizeInSquareMeter { get; set; }

        public CubeInfo(double temperature, TemperatureUnit temperatureUnit, double energyPerMonth, EnergyUnit energyUnit, double solarPanelSizeInSquareMeter)
        {
            // 'temperatureConverter' and 'TemperatureUnit' need to be set before 'Temperature'. As both values are necessary for setting the 'Temperature'.

            temperatureConverter = new TemperatureConverter();

            TemperatureUnit = temperatureUnit;
            Temperature = temperature;

            // 'EnergyUnit' and 'SolarPanelSizeInSquareMeter' need to be set before 'EnergyPerMonth'. As both values are necessary for setting the 'EnergyPerMonth'.

            EnergyUnit = energyUnit;
            SolarPanelSizeInSquareMeter = solarPanelSizeInSquareMeter;
            EnergyPerMonth = energyPerMonth;
        }
    }
}
