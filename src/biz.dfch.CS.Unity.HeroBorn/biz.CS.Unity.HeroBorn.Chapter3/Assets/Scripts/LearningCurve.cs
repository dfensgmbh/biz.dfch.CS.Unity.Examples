using UnityEngine;
namespace Assets.Scripts

{
    public class LearningCurve : MonoBehaviour
    {
        private int currentAge = 30;
        public int AddedAge = 1;

        public float Pi = 3.14f;
        public string WelcomeMessage = "Welcome to my Unity Game";
        public bool IsMarked = true;

        // Start is called before the first frame update
        void Start()
        {
            ComputeAge();
            var isGenerated = GenerateCharacter("Yasuo");
        }

        /// <summary>
        /// Computes a modified age integer
        /// </summary>
        void ComputeAge()
        {
            var computedAge = currentAge + AddedAge;

            Debug.Log("Log: Computed Age: " + computedAge);
            Debug.Log($"Log: Computed Age: {computedAge}");
            Debug.LogFormat("LogFormat: Computed Age: {0}", computedAge);
        }

        public bool GenerateCharacter(string characterName)
        {
            Debug.Log($"{characterName} - Character generated");

            return true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
