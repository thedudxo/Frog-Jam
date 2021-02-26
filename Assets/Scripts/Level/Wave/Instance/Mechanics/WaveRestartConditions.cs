using System.Collections;
using UnityEngine;

namespace waveScripts
{
    public class WaveRestartConditions : MonoBehaviour
    {
        [SerializeField] Wave wave;

        Transform waveTransform;
        bool ReachedEndOfLevel => waveTransform.position.x > wave.level.end;
        bool shouldRestart = false;


        private void Start()
        {
            waveTransform = wave.transform;
        }

        private void Update()
        {
            if (wave.state == Wave.State.normal)
            {
                if (ReachedEndOfLevel) shouldRestart = true;

                if (shouldRestart)
                {
                    shouldRestart = false;
                    wave.BreakWave();
                }
            }
        }

        public void TriggerRestart()
        {
            shouldRestart = true;
        }
    }
}