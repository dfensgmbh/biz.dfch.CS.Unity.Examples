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

using Assets.Constants;

namespace Assets.Calculators
{
    public class Calculator
    {
        public static int CalculateFontSize(float scale)
        {
            return (int) ((scale - CalculationValue.MinCubeScaleValue) / CalculationValue.CubeScaleValueRange * CalculationValue.FontSizeRange + CalculationValue.MinFontSize);
        }

        public static float CalculateCubeScaleValue(double energyPerMonth)
        {
            return (float)(CalculationValue.EnergyRange - (CalculationValue.MaxEnergyPerSquareMeterPerOneMonth - energyPerMonth)) / CalculationValue.EnergyRange * CalculationValue.CubeScaleValueRange;
        }
        public static float CalculateRedColorValue(double temperature)
        {
            return (float)((CalculationValue.TemperatureRangeInKelvin - (CalculationValue.MaxKelvinTemperature - temperature)) / CalculationValue.TemperatureRangeInKelvin);
        }
    }
}
