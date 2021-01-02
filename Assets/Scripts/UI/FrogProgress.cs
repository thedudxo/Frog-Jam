using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using LevelScripts;

namespace FrogScripts {
    public class FrogProgress : MonoBehaviour
    {
        [SerializeField] Frog frog;
        //[SerializeField] ProgressBar progressBar;

        [SerializeField] Slider playerProgressBar;
        [SerializeField] Slider progressLost;
        const float progressLostDecaySpeed = 0.005f;
        [SerializeField] Slider personalBest;

        Level level;


        private void Start()
        {
            level = frog.currentLevel;
        }

        private void Update()
        {
            /* each player has their own progress bar
             * each players bar displays themselves emphasised over the other players
             * colours in this players progress & progrees lost behind all players
             */
            float frogPosX = frog.transform.position.x;
            playerProgressBar.value = (frogPosX - level.startLength) / (level.end - level.startLength);

            //update looseProgressBar
            if (progressLost.gameObject.activeInHierarchy)
            {
                progressLost.value -= progressLostDecaySpeed;

                if (progressLost.value <= playerProgressBar.value)
                {
                    progressLost.gameObject.SetActive(false);
                }
            }
        }

        public void FrogRespawned()
        {
            CheckPersonalBest();
            LooseProgress();
        }

        void CheckPersonalBest()
        {
            if (!personalBest.gameObject.activeInHierarchy)
            {
                personalBest.gameObject.SetActive(true);

            }

            if (personalBest.value < playerProgressBar.value)
            {
                personalBest.value = playerProgressBar.value;
            }
        }

        void LooseProgress()
        {
            progressLost.gameObject.SetActive(true);
            progressLost.value = playerProgressBar.value;
        }

        public float GetPersonalBest()
        {
            return personalBest.value;
        }
    }
}