﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using LevelScripts;

namespace FrogScripts {
    public class FrogProgress : MonoBehaviour , INotifyOnAnyRespawn
    {
        [SerializeField] Frog frog;

        [SerializeField] Slider playerProgressBar;
        [SerializeField] Slider progressLost;
        const float progressLostDecaySpeed = 0.002f;
        [SerializeField] Slider personalBest;
        [SerializeField] Slider waveProgressBar;

        Level level;
        Transform waveTransform;

        private void Start()
        {
            level = frog.currentLevel;
            waveTransform = level.wave.transform;
            frog.SubscribeOnAnyRespawn(this);

            AddOtherPlayers();

            void AddOtherPlayers()
            {
                foreach(Frog frog in frog.frogManager.Frogs)
                {

                }
            }
        }

        private void Update()
        {
            /* each player has their own progress bar - DONE
             * each players bar displays themselves emphasised over the other players - NOT DONE
             * colours in this players progress & progrees lost behind all players
             */


            PlayerProgress();
            WaveProgress();
            LooseProgress();

            void PlayerProgress()
            {
                float frogPosX = frog.transform.position.x;
                playerProgressBar.value = (frogPosX - level.startLength) / (level.end - level.startLength);
            }

            void WaveProgress()
            {
                float wavePosX = waveTransform.position.x;
                waveProgressBar.value = (wavePosX - level.startLength) / (level.end - level.startLength);
            }

            void LooseProgress()
            {
                if (progressLost.gameObject.activeInHierarchy)
                {
                    progressLost.value -= progressLostDecaySpeed;

                    if (progressLost.value <= playerProgressBar.value)
                        progressLost.gameObject.SetActive(false);
                }
            }
        }

        public void OnAnyRespawn()
        {
            CheckPersonalBest();
            LooseProgress();

            void CheckPersonalBest()
            {
                if (!personalBest.gameObject.activeInHierarchy)
                    personalBest.gameObject.SetActive(true);


                if (personalBest.value < playerProgressBar.value)
                    personalBest.value = playerProgressBar.value;
            }

            void LooseProgress()
            {
                progressLost.gameObject.SetActive(true);
                progressLost.value = playerProgressBar.value;
            }
        }
    }
}