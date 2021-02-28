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
        bool shouldBreak = false;


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

                if (shouldBreak)
                {
                    shouldBreak = false;
                    wave.BreakWave();
                }
            }

            void CheckUpdateConditions()
            {
                //these could probably be optimised if needed
                if (
                    ReachedEndOfLevel ||
                    frogMediator.AnyFrogAhead(wave) == false ||
                    frogMediator.AllFrogsOnPlatform()
                    ) 

                    shouldBreak = true;
            }
        }

        public void TriggerRestart()
        {
            shouldBreak = true;
        }
    }
}