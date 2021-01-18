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

namespace Assets.Constants
{
    public class CalculationValue
    {        
        public const int MaxFontSize = 125;             
        public const int MinFontSize = 40;
        public const int FontSizeRange = MaxFontSize - MinFontSize;
        // Average gain of solar energy in swiss alps is 135 kWh/m2 during April  
        public const int MaxEnergyPerSquareMeterPerOneMonth = 150;
        public const int MinEnergyPerSquareMeterPerOneMonth = 0;
        public const int EnergyRange = MaxEnergyPerSquareMeterPerOneMonth - MinEnergyPerSquareMeterPerOneMonth;
        public const int MaxCubeScaleValue = 2;
        public const int MinCubeScaleValue = 0;
        public const int CubeScaleValueRange = MaxCubeScaleValue - MinCubeScaleValue;
        // Highest measured temperature on earth
        public const int MaxKelvinTemperature = 330;
        // Lowest measured temperature on earth
        public const int MinKelvinTemperature = 184;
        public const int TemperatureRangeInKelvin = MaxKelvinTemperature - MinKelvinTemperature;
        public const float GroundXScaleValueBase = 0.2f;
        public const float GroundXScaleValue = 0.3f;
    }
}
