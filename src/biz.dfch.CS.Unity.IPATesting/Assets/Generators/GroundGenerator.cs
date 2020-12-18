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

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;

namespace Assets.Generators
{
    public class GroundGenerator
    {
        private readonly List<GameObject> cubesOnScene;
        private readonly GameObject groundGameObject;

        public GroundGenerator(List<GameObject> cubesOnScene, GameObject groundGameObject)
        {
            Contract.Assert(null != cubesOnScene);
            Contract.Assert(!cubesOnScene.Any());
            Contract.Assert(null != groundGameObject);

            this.cubesOnScene = cubesOnScene.OrderBy(go => go.transform.position.x).ToList();
            this.groundGameObject = groundGameObject;
        }

        public void Generate()
        {
            var firstCubeXPosition = cubesOnScene.First().transform.position.x;
            var lastCubeXPosition = cubesOnScene.Last().transform.position.x;

            RecalculateGroundXValue(firstCubeXPosition, lastCubeXPosition);
        }

        private void RecalculateGroundXValue(float firstCubeXPosition, float lastCubeXPosition)
        {
            var newGroundXPosition = PropertyCalculator.CalculateAverage(firstCubeXPosition, lastCubeXPosition);
            var newXScaleValue = PropertyCalculator.CalculateGroundXScaleValue(cubesOnScene.Count);

            var groundLocalScale = groundGameObject.transform.localScale;
            var groundPosition = groundGameObject.transform.position;

            groundPosition.x = newGroundXPosition;
            groundGameObject.transform.position = groundPosition;

            groundLocalScale.x = newXScaleValue;
            groundGameObject.transform.localScale = groundLocalScale;
        }
    }
}
