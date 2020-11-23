using System.Collections.Generic;
using System.Linq;
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

        public int CurrentGold = 52;

        public bool PureOfHeart = true;
        public bool HasSecretIncantation = false;
        public string RareItem = "Infinity Edge";


        // Start is called before the first frame update
        void Start()
        {

            int[] topScore = {213, 233, 245};
            List<string> topScorer = new List<string> {"Messi", "Ronlado", "Haaland"};
            Dictionary<int, string> bestScorer = new Dictionary<int, string>
            {
                {213, "Messi"},
                {233, "Ronaldo"},
                {245, "Haaland"}
            };

            Debug.Log($"Top Score {topScore[2]}");
            Debug.Log($"Top Scorer {topScorer.LastOrDefault()}");
            Debug.Log($"Dictionary best Scorer {bestScorer.Values.LastOrDefault()}");

            for (int i = 0; i < topScorer.Count; i++)
            {
                Debug.Log($"Score: {topScore[i]} Scorer: {topScorer[i]}");
            }

            foreach (var scorer in topScorer)
            {
                Debug.Log($"Scorer is: {scorer}");
            }

            foreach (KeyValuePair<int, string> keyValuePair in bestScorer)
            {
                Debug.Log($"{keyValuePair.Value} scored {keyValuePair.Key} points");
            }

            var playerLive = 5;
            while (playerLive > 0)
            {
                Debug.Log("Fight!");
                playerLive--;
            }
            Debug.Log("Dead.");

            OpenTreasureChamber();

            if (CurrentGold > 50)
            {
                Debug.Log("Enough Gold");
            }
            else if (CurrentGold < 15)
            {
                Debug.Log("Poor guy");
            }
            else
            {
                Debug.Log("Ha!?!");
            }

            ComputeAge();
            var isGenerated = GenerateCharacter("Yasuo");


            switch (RareItem)
            {
                case "Death Cap":
                    Debug.Log("Mage");
                    break;
                case "Infinity Edge":
                    Debug.Log("ADC");
                    break;
                default:
                    Debug.Log("?");
                    break;
            }
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

        public void OpenTreasureChamber()
        {
            if (PureOfHeart && RareItem == "Infinity Edge")
            {
                Debug.Log("Welcome!");

                if (HasSecretIncantation)
                {
                    Debug.Log("Lets Go!");
                }
                else
                {
                    Debug.Log("Go Back");
                }
            }
            else
            {
                    Debug.Log("Wrong");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
