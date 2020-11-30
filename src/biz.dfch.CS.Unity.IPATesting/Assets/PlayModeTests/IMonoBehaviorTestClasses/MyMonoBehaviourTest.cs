using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests.IMonoBehaviorTestClasses
{
    public class MyMonoBehaviourTest : MonoBehaviour, IMonoBehaviourTest
    {
        private int frameCount;
        public bool IsTestFinished => frameCount > 10;

        void Update()
        {
            Debug.Log("Update");
            frameCount++;
            Debug.Log("Update 2");
        }
    }
}
