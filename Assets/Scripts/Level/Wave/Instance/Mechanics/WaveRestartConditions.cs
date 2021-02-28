using System.Collections;
using UnityEngine;

namespace waveScripts
{
    public class WaveRestartConditions : MonoBehaviour
    {
        [SerializeField] Wave wave;
        WaveFrogMediatior frogMediator;

        Transform waveTransform;
        bool ReachedEndOfLevel => waveTransform.position.x > wave.level.end;
        bool shouldRestart = false;


        private void Start()
        {
            waveTransform = wave.transform;
            frogMediator = wave.manager.frogMediatior;
        }

        private void Update()
        {
            if (wave.state == Wave.State.normal)
            {
                CheckUpdateConditions();

                if (shouldRestart)
                {
                    shouldRestart = false;
                    wave.BreakWave();
                }
            }

            void CheckUpdateConditions()
            {

                if (
                    ReachedEndOfLevel ||
                    frogMediator.AnyFrogAhead(wave) == false
                    ) 

                    shouldRestart = true;
            }
        }

        public void TriggerRestart()
        {
            shouldRestart = true;
        }
    }
}