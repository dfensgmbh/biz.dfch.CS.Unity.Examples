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
        // ReSharper disable once InconsistentNaming
        public const int MaxFontSize = 125;

        // ReSharper disable once InconsistentNaming
        public const int MinFontSize = 40;

        // ReSharper disable once InconsistentNaming
        public const int FontSizeRange = MaxFontSize - MinFontSize;

        // ReSharper disable once InconsistentNaming
        // Average gain of solar energy in swiss alps is 135 kWh/m2 during April  
        public const int MaxEnergyPerSquareMeterPerOneMonth = 150;

        // ReSharper disable once InconsistentNaming
        public const int MinEnergyPerSquareMeterPerOneMonth = 0;

        // ReSharper disable once InconsistentNaming
        public const int EnergyRange = MaxEnergyPerSquareMeterPerOneMonth - MinEnergyPerSquareMeterPerOneMonth;

        // ReSharper disable once InconsistentNaming
        public const int MaxCubeScaleValue = 2;

        // ReSharper disable once InconsistentNaming
        public const int MinCubeScaleValue = 0;

        // ReSharper disable once InconsistentNaming
        public const int CubeScaleValueRange = MaxCubeScaleValue - MinCubeScaleValue;

        // ReSharper disable once InconsistentNaming
        // Highest measured temperature on earth
        public const int MaxKelvinTemperature = 330;

        // ReSharper disable once InconsistentNaming
        // Lowest measured temperature on earth
        public const int MinKelvinTemperature = 184;

        // ReSharper disable once InconsistentNaming
        public const int TemperatureRangeInKelvin = MaxKelvinTemperature - MinKelvinTemperature;

        // ReSharper disable once InconsistentNaming
        public const float GroundXScaleValueBase = 0.2f;
        
        // ReSharper disable once InconsistentNaming
        public const float GroundXScaleValue = 0.3f;
    }
}
