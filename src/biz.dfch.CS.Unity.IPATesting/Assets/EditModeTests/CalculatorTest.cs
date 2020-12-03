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
using Assets.Calculators;
using NUnit.Framework;

namespace Assets.EditModeTests
{
    public class CalculatorTest
    {
        [TestCase(2f, 125)]
        [TestCase(0f, 40)]
        [TestCase(1f, 83)]
        public void CalculatingFontSizeReturnsExpectedResult(float scaleValue, int expectedFontSize)
        {
            // Arrange
            // Act
            var result = Calculator.CalculateFontSize(scaleValue);

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
            Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.CalculateFontSize(scaleValue));
        }

        [TestCase(150d, 2f)]
        [TestCase(0d, 0f)]
        public void CalculatingCubeScaleValueReturnsExpectedResult(double energy, float expectedCubeScaleValue)
        {
            // Arrange
            // Act
            var result = Calculator.CalculateCubeScaleValue(energy);

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
            Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.CalculateCubeScaleValue(energy));
        }

        [TestCase(330d, 1f)]
        [TestCase(184, 0f)]
        public void CalculatingRedColorValueReturnsExpectedResult(double temperatureInKelvin, float expectedRedColorValue)
        {
            // Arrange
            // Act
            var result = Calculator.CalculateRedColorValue(temperatureInKelvin);

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
            Assert.Throws<ArgumentOutOfRangeException>(() => Calculator.CalculateRedColorValue(temperatureInKelvin));
        }
    }
}
