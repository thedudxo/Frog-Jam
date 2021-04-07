using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WaveScripts;
using Chaseables;

namespace FrogScripts.Progress
{
    public class WaveProgress : MonoBehaviour, IProgressTracker
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogChaseable chaseable;
        [SerializeField] Slider waveProgressBar;

        public void UpdateProgress()
        {
            IChaser chaser = chaseable.ActiveChaser;
            if (chaser == null)
            {
                waveProgressBar.value = 0;
                return;
            }

            float wavePosX = chaser.GetXPos();
            LevelScripts.Level level = frog.currentLevel;
            waveProgressBar.value = (wavePosX - level.StartPlatformLength) / (level.region.end - level.StartPlatformLength);
        }
    }
}