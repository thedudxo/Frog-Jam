using UnityEngine;
using UnityEngine.UI;
using Pursuits;

namespace FrogScripts.Progress
{
    public class WaveProgress : MonoBehaviour, IProgressTracker
    {
        [SerializeField] Frog frog;
        [SerializeField] Slider waveProgressBar;

        public void UpdateProgress()
        {
            Pursuer pursuer = frog.FrogRunner.runner?.pursuerBehind;
            if (pursuer == null)
            {
                waveProgressBar.value = 0;
                return;
            }

            float wavePosX = pursuer.position;
            LevelScripts.Level level = frog.currentLevel;
            waveProgressBar.value = (wavePosX - level.StartPlatformLength) / (level.region.end - level.StartPlatformLength);
        }
    }
}