using Levels;
using UnityEngine;

namespace Frogs {
    public class FrogLevelControlls : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] LevelStats levelStats;
        [SerializeField] SplitEffectsManager splitFX;
        [SerializeField] FrogTime frogTime;

        [SerializeField] LevelEndScreen levelEndScreen;
        [SerializeField] Rigidbody2D rb;


        bool PlayerGotToTheEnd => frog.transform.position.x >= frog.currentLevel.region.end;
        bool PlayerInputRestart => Input.GetKeyDown(frog.controllers.controlls.suicideKey);

        bool playingLevel = true;

        private void Update()
        { 
            switch (playingLevel)
            {
                case true:
                    if (PlayerGotToTheEnd) FinishLevel();
                    break;

                case false:
                    if (PlayerInputRestart) RestartLevel();
                    break;
            }
        }

        public void RestartLevel()
        {
            levelEndScreen.Disable();
            playingLevel = true;
            frog.controllers.life.Restart();
        }

        public void FinishLevel()
        {
            foreach (INotifyOnEndLevel subscriber in frog.events.toNotifyOnEndLevel)
                subscriber.OnEndLevel();

            levelStats.CheckForPBTime();
            playingLevel = false;
            EnableEndScreen();
        }

        private void EnableEndScreen()
        {
            levelEndScreen.Enable(frogTime.CurrentLevelTime, (float) levelStats.PbTime, splitFX.TotalSplitTime);
        }
    }
}
