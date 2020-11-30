using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.PlayModeTests
{
    public class MyMonoBehaviourTest : MonoBehaviour, IMonoBehaviourTest
    {
        private int frameCount;
        public bool IsTestFinished => frameCount > 10;

        void Update()
        {
            frameCount++;
        }
    }
}
