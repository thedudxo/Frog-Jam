using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using waveScripts;

namespace FrogScripts
{
    public class FrogWaveInteractions : MonoBehaviour, INotifyOnLeftPlatform, INotifyOnDeath, INotifyOnEndLevel
    {
        [SerializeField] public Frog frog;
        WaveManager waveManager;
        FrogManager frogManager;

        WaveFrogMediatior waveMediator;

        Wave attachedWave;

        private void Start()
        {
            frogManager = frog.manager;
            waveManager = frog.currentLevel.waveManager;
            waveMediator = frog.currentLevel.waveFrogMediatior;

            frog.events.SubscribeOnLeftPlatform(this);
            frog.events.SubscribeOnDeath(this);
            frog.events.SubscribeOnEndLevel(this);
        }
        public void OnLeftPlatform()
        {
            float frogPosX = frog.transform.position.x;
            attachedWave = waveMediator.ClosestWaveBehindPosition(frogPosX);
        }

        public void OnDeath()
        {
            attachedWave.breakControlls.FrogTriggerBreak();
        }
        public void OnEndLevel()
        {
            attachedWave.breakControlls.FrogTriggerBreak();
        }
    }
}