using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using waveScripts;

namespace FrogScripts
{
    public class FrogWaveInteractions : MonoBehaviour, INotifyOnLeftPlatform, INotifyOnDeath, INotifyOnEndLevel
    {
        [SerializeField] public Frog frog;

        WaveFrogMediatior waveMediator;

        Wave attachedWave;

        private void Start()
        {
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
            waveMediator.CheckIfWaveShouldBreak(attachedWave);
        }
        public void OnEndLevel()
        {
            waveMediator.CheckIfWaveShouldBreak(attachedWave);
        }

    }
}