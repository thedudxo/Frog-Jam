using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts.Progress
{
    public class WaveProgress : MonoBehaviour, IProgressTracker
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogWaveInteractions waveInteractions;
        [SerializeField] Slider waveProgressBar;

        public void UpdateProgress()
        {
            waveScripts.Wave wave = waveInteractions.attachedWave;
            if (wave == null || wave.state == waveScripts.Wave.State.inactive)
            {
                waveProgressBar.value = 0;
                return;
            }

            float wavePosX = wave.transform.position.x;
            LevelScripts.Level level = frog.currentLevel;
            waveProgressBar.value = (wavePosX - level.startLength) / (level.end - level.startLength);
        }
    }
}