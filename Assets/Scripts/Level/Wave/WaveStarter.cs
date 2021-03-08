using System.Collections;
using UnityEngine;

namespace waveScripts
{
    public class WaveStarter
    {
        WaveManager waveManager;
        WaveFrogMediatior frogMediatior;

        bool canStartRightNow = true;

        public WaveStarter(WaveManager waveManager, WaveFrogMediatior frogMediatior)
        {
            this.waveManager = waveManager; 
            this.frogMediatior = frogMediatior;
        }

        public void CheckStartConditions()
        {
            if (canStartRightNow) return;
            canStartRightNow = true;

            NoWavesBehindLastFrog();

            void NoWavesBehindLastFrog()
            {
                if (frogMediatior.NoWaveBehindLastFrog() == false)
                    canStartRightNow = false;
            }
        }

        public void StartWave()
        {
            if (canStartRightNow)
                waveManager.GetInactiveWave().StartWave();

            canStartRightNow = false;
        }
    }
}