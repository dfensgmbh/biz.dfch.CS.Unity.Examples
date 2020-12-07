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
using NUnit.Framework;

namespace Assets.EditModeTests
{
    public class PropertyCalculatorTest
    {
        [TestCase(2f, 125)]
        [TestCase(0f, 40)]
        [TestCase(1f, 83)]
        public void CalculatingFontSizeReturnsExpectedResult(float scaleValue, int expectedFontSize)
        {
            // Arrange
            // Act
            var result = PropertyCalculator.CalculateFontSize(scaleValue);

            // Assert
            Assert.AreEqual(expectedFontSize, result);
        }

        [TestCase(3f)]
        [TestCase(2.1f)]
        [TestCase(-1f)]
        [TestCase(-0.1f)]
        public void CalculatingFontSizeWithScaleValueOutOfRangeThrowsArgumentOutOfRangeException(float scaleValue)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => PropertyCalculator.CalculateFontSize(scaleValue));
        }

        [TestCase(150d, 2f)]
        [TestCase(0d, 0f)]
        public void CalculatingCubeScaleValueReturnsExpectedResult(double energy, float expectedCubeScaleValue)
        {
            // Arrange
            // Act
            var result = PropertyCalculator.CalculateCubeScaleValue(energy);

            // Assert
            Assert.AreEqual(expectedCubeScaleValue, result);
        }

        [TestCase(151f)]
        [TestCase(150.1f)]
        [TestCase(-1f)]
        [TestCase(-0.1f)]
        public void CalculatingCubeScaleValueWithValueOutOfRangeThrowsArgumentOutOfRangeException(float energy)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => PropertyCalculator.CalculateCubeScaleValue(energy));
        }

        [TestCase(330d, 1f)]
        [TestCase(184, 0f)]
        public void CalculatingRedColorValueReturnsExpectedResult(double temperatureInKelvin, float expectedRedColorValue)
        {
            // Arrange
            // Act
            var result = PropertyCalculator.CalculateRedColorValue(temperatureInKelvin);

            // Assert
            Assert.AreEqual(expectedRedColorValue, result);
        }

        [TestCase(331d)]
        [TestCase(330.1d)]
        [TestCase(183)]
        [TestCase(183.9d)]
        public void CalculatingRedColorValueWithTemperatureValueOutOfRangeThrowsArgumentOutOfRangeException(double temperatureInKelvin)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => PropertyCalculator.CalculateRedColorValue(temperatureInKelvin));
        }


        [TestCase(100, 2, 50)]
        [TestCase(100, 0.5, 200)]
        [TestCase(200, 2.5, 80)]
        [TestCase(30, 0.1, 300)]
        public void CalculatingEnergyForOneSquareMeterReturnsExpectedResult(double energy, double solarPanelSizeInSquareMeter, double expectedEnergyPerSquareMeter)
        {
            // Arrange
            // Act
            var result = PropertyCalculator.CalculateEnergyPerOneSquareMeter(energy, solarPanelSizeInSquareMeter);

            // Assert
            Assert.AreEqual(expectedEnergyPerSquareMeter, result);
        }
    }
}
