using System.Collections;
using UnityEngine;

namespace waveScripts
{
    public class WaveRestartConditions : MonoBehaviour
    {
        [SerializeField] Wave wave;
        WaveFrogMediatior frogMediator;

        Transform waveTransform;

        bool shouldBreak = false;
        bool ReachedEndOfLevel => waveTransform.position.x > wave.level.end;
        bool NoFrogsAhead => frogMediator.AnyFrogAhead(wave) == false;
        bool AllFrogsOnPlatform => frogMediator.AllFrogsOnPlatform();

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
                    wave.BreakWave();
                    shouldBreak = false;
                }
            }

            void CheckUpdateConditions()
            {
                //these could probably be optimised if needed
                if (
                    ReachedEndOfLevel ||
                    NoFrogsAhead ||
                    AllFrogsOnPlatform
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