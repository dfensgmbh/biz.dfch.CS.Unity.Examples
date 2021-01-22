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

using Assets.AutoMapper;
using Assets.Constants;
using Assets.Models;
using AutoMapper;
using NUnit.Framework;

namespace Assets.EditModeTests.AutoMapper
{
    public class AutoMapperTest
    {
        [Test]
        public void AutoMapperSuccessfullyMapsCsvDataToCubeInfo()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperDefaultProfile>());
            var sut = new Mapper(config);

            var csvData = new CsvData(30d, TemperatureUnit.Celsius, 50d, EnergyUnit.KiloWatt, 1d);
        
            // Act
            var result = sut.Map<CubeInfo>(csvData);
        
            // Assert
            Assert.AreEqual(result.TemperatureUnit, csvData.TemperatureUnit);
            Assert.AreEqual(result.Temperature, csvData.HouseTemperature);
            Assert.AreEqual(result.EnergyPerMonth, csvData.EnergyPerMonth);
            Assert.AreEqual(result.EnergyUnit, csvData.EnergyUnit);
            Assert.AreEqual(result.SolarPanelSizeInSquareMeter, csvData.SolarPanelSizeInSquareMeter);
        }
    }
}
