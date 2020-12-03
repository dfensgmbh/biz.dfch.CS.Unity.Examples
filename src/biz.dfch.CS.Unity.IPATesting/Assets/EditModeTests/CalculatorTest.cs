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

using System.Collections;
using System.Runtime.InteropServices;
using Assets.Calculators;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Assets.EditModeTests
{
    public class CalculatorTest
    {
        [TestCase(2d, 125)]
        [TestCase(0d, 40)]
        public void CalculatingFontSizeReturnsExpectedResult(double scaleValue, int expectedFontSize)
        {
            // Arrange

            // Act
            var result = Calculator.CalculateRedColorValue(scaleValue);

            // Assert
            Assert.AreEqual(expectedFontSize, result);
        }

        [TestCase(150d, 2f)]
        [TestCase(0d, 0f)]
        public void CalculatingCubeScaleValueReturnsExpectedResult(double energy, float expectedCubeScaleValue)
        {
            // Arrange

            // Act
            var result = Calculator.CalculateRedColorValue(energy);

            // Assert
            Assert.AreEqual(expectedCubeScaleValue, result);
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
    }
}
