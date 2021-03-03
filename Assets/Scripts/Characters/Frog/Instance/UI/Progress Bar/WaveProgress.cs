using System.Collections;
using UnityEngine;

namespace FrogScripts.Progress
{
    public class WaveProgress : MonoBehaviour, IProgressTracker
    {
        [SerializeField] FrogProgress frogProgress;
        WaveFrogMediatior waveMediator;

        private void Start()
        {
            waveMediator = frogProgress.frog.currentLevel.waveFrogMediatior;
        }

        public void UpdateProgress()
        {
            //float wavePosX = waveTransform.position.x;
            //waveProgressBar.value = (wavePosX - level.startLength) / (level.end - level.startLength);
        }
    }
}