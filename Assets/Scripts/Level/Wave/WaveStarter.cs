using System.Collections;
using UnityEngine;

namespace waveScripts
{
    public class WaveStarter
    {
        WaveManager waveManager;
        WaveFrogMediatior frogMediatior;

        float waveStartCooldown = .5f;
        float startTimer = 0;
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

            //TimerElapsed();
            NoWavesBehindLastFrog();

            void TimerElapsed()
            {
                startTimer += Time.deltaTime;

                if (startTimer > waveStartCooldown)
                    startTimer = 0;
                else
                    canStartRightNow = false;
            }

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