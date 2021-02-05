using LevelScripts;
using UnityEngine;

namespace FrogScripts {
    public class FrogLevelControlls : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] LevelStats levelStats;
        [SerializeField] SplitEffectsManager splitFX;
        [SerializeField] FrogTime frogTime;

        [SerializeField] LevelEndScreen levelEndScreen;
        [SerializeField] Rigidbody2D rb;


        bool PlayerGotToTheEnd => frog.transform.position.x >= frog.currentLevel.end;
        bool PlayerInputRestart => Input.GetKeyDown(KeyCode.Q);

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
            GM.gameState = GM.GameState.playingLevel;
            playingLevel = true;
            frog.RestartLevel();
        }

        public void FinishLevel()
        {
            levelStats.CheckForPBTime();
            playingLevel = false;
            EnableEndScreen();
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }

        private void EnableEndScreen()
        {
            levelEndScreen.Enable(frogTime.CurrentLevelTime, (float) levelStats.PbTime, splitFX.TotalSplitTime);
        }
    }
}
