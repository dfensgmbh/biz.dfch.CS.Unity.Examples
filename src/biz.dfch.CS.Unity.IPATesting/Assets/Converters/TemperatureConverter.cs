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

using System.IO;
using Assets.Constants;

namespace Assets.Converters
{
    public class TemperatureConverter
    {
        private const double DeltaKelvinCelsius = 273.15d;

        public double ConvertToKelvin(double temperature, TemperatureUnit temperatureUnit)
        {
            return temperatureUnit switch
            { 
                TemperatureUnit.Celsius => temperature + DeltaKelvinCelsius,
                TemperatureUnit.Fahrenheit => (temperature - 32) * 5 / 9 + DeltaKelvinCelsius,
                TemperatureUnit.None => throw new InvalidDataException(),
                _ => temperature
            };
        }

        public double ConvertToFahrenheit(double temperature, TemperatureUnit temperatureUnit)
        {
            return temperatureUnit switch
            {
                TemperatureUnit.Celsius => temperature * 9 / 5 + 32,
                TemperatureUnit.Kelvin => (temperature - DeltaKelvinCelsius) * 9 / 5 + 32,
                TemperatureUnit.None => throw new InvalidDataException(),
                _ => temperature
            };
        }       
        
        public double ConvertToCelsius(double temperature, TemperatureUnit temperatureUnit)
        {
            return temperatureUnit switch
            {
                TemperatureUnit.Kelvin => temperature - DeltaKelvinCelsius,
                TemperatureUnit.Fahrenheit => (temperature - 32) * 5 / 9,
                TemperatureUnit.None => throw new InvalidDataException(),
                _ => temperature
            };
        }
    }
}
