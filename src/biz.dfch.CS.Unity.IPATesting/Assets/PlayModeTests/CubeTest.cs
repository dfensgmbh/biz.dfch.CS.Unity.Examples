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

using NUnit.Framework;
using UnityEngine;

namespace Assets.PlayModeTests
{
    public static class CubeTest
    {
        public static void HasExpectedData(GameObject cube, ExpectedCubeData expectedCubeData)
        {
            var cubeRenderer = cube.GetComponent<Renderer>();
            var meshFilter = cube.GetComponent<MeshFilter>();
            var boxCollider = cube.GetComponent<BoxCollider>();
            var textMesh = cube.GetComponentInChildren<TextMesh>();

            // Color Tests

            var expectedColor = expectedCubeData.ExpectedColor;
            var resultColor = cubeRenderer.material.GetColor("_Color");

            Debug.Log($"Expected Color: '{expectedColor}'");
            Debug.Log($"Result Color: '{resultColor}'");

            Assert.AreEqual(expectedColor, resultColor);

            // Scale Tests

            var expectedVertices = expectedCubeData.ExpectedVertices;
            var resultVertices = meshFilter.mesh.vertices;

            for (int i = 0; i < expectedVertices.Length; i++)
            {
                var expectedVertex = expectedVertices[i];
                var vertexToBeAsserted = resultVertices[i];

                Debug.Log($"Expected vertex: '{expectedVertex}'");
                Debug.Log($"Vertex to be asserted: '{vertexToBeAsserted}'");

                Assert.AreEqual(expectedVertex, vertexToBeAsserted);
            }

            // BoxCollider Size Tests

            var expectedBoxColliderSize = expectedCubeData.ExpectedBoxColliderSize;
            var resultBoxColliderSize = boxCollider.size;

            Assert.AreEqual(expectedBoxColliderSize, resultBoxColliderSize);

            // Text Tests

            Debug.Log("Children Count: " + cube.GetComponentsInChildren<Component>().Length);

            var expectedText = expectedCubeData.ExpectedText;
            var resultText = textMesh.text;

            Debug.Log($"Expected text: '{expectedText}'");
            Debug.Log($"Result text: '{resultText}'");

            Assert.AreEqual(expectedText, resultText);

            var expectedFontSize = expectedCubeData.ExpectedFontSize;
            var resultFontSize = textMesh.fontSize;

            Debug.Log($"Expected font size: '{expectedFontSize}'");
            Debug.Log($"Result font size: '{resultFontSize}'");

            Assert.AreEqual(expectedFontSize, resultFontSize);

            Debug.Log("Test Finished");
        }
    }
}
