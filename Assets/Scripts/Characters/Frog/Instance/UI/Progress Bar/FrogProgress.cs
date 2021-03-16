using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using LevelScripts;
using System.Collections.Generic;

namespace FrogScripts.Progress
{
    public interface IProgressTracker
    {
        void UpdateProgress();
    }

    public class FrogProgress : MonoBehaviour , INotifyOnAnyRespawn
    {
        [SerializeField] public Frog frog;

        [SerializeField] Slider playerProgressBar;
        [SerializeField] Slider progressLost;
        const float progressLostDecaySpeed = 0.002f;
        [SerializeField] Slider personalBest;
        [SerializeField] Slider waveProgressBar;

        [SerializeField] List<MonoBehaviour> progressTrackerComponents;
        List<IProgressTracker> progressTrackers = new List<IProgressTracker>();

        Level level;

        private void Start()
        {
            foreach(MonoBehaviour obj in progressTrackerComponents)
            {
                IProgressTracker tracker = obj as IProgressTracker;
                if (tracker != null)
                {
                    progressTrackers.Add(tracker);
                }
            }

            level = frog.currentLevel;
            frog.events.SubscribeOnAnyRespawn(this);

            AddOtherPlayers();

            void AddOtherPlayers()
            {
                foreach(Frog frog in frog.manager.Frogs)
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

            foreach (IProgressTracker tracker in progressTrackers)
            {
                tracker.UpdateProgress();
            }


            //old
            PlayerProgress();
            LooseProgress();

            void PlayerProgress()
            {
                float frogPosX = frog.transform.position.x;
                playerProgressBar.value = (frogPosX - level.startLength) / (level.end - level.startLength);
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