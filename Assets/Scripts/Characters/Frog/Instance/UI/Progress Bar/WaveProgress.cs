using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WaveScripts;

namespace FrogScripts.Progress
{
    public class WaveProgress : MonoBehaviour, IProgressTracker
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogWaveInteractions waveInteractions;
        [SerializeField] Slider waveProgressBar;

        public void UpdateProgress()
        {
            Wave wave = waveInteractions.AttachedWave;
            if (wave == null || wave.state == WaveScripts.Wave.State.inactive)
            {
                waveProgressBar.value = 0;
                return;
            }

            float wavePosX = wave.transform.position.x;
            LevelScripts.Level level = frog.currentLevel;
            waveProgressBar.value = (wavePosX - level.StartPlatformLength) / (level.region.end - level.StartPlatformLength);
        }
    }
}