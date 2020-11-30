﻿/**
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
using Assets.PlayModeTests.IMonoBehaviorTestClasses;
using Assets.Scripts;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests
{
    public class CubeBehaviourTest
    {
        [UnityTest]
        public IEnumerator CubeBehaviour_Works()
        {
            yield return new MonoBehaviourTest<CubeBehaviorWorksTest>();
        }

        [UnityTest]
        public IEnumerator MonoBehaviourTest_Works()
        {
            yield return new MonoBehaviourTest<MyMonoBehaviourTest>();
        }
    }
}