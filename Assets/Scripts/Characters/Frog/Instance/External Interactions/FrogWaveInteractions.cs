using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveScripts;

namespace FrogScripts
{
    public class FrogWaveInteractions : MonoBehaviour, INotifyOnLeftPlatform, INotifyOnDeath, INotifyOnEndLevel, INotifyOnRestart
    {
        [SerializeField] public Frog frog;
        WaveCollection waveManager;
        FrogManager frogManager;

        WaveFrogMediatior waveMediator;

        public Wave AttachedWave { get; private set; }

        private void Start()
        {
            frogManager = frog.manager;
            waveManager = frog.currentLevel.waveManager;
            waveMediator = frog.currentLevel.waveFrogMediatior;

            frog.events.SubscribeOnLeftPlatform(this);
            frog.events.SubscribeOnDeath(this);
            frog.events.SubscribeOnEndLevel(this);
            frog.events.SubscribeOnRestart(this);
        }

        private void Update()
        {
            bool frogNotOnStartPlatform = frog.state != FrogState.State.StartPlatform;

            if (frogNotOnStartPlatform) //minor bug: can be on start platform and dying, causing this to trigger
            {
                if (AttachedWave == null) AttachClosestWave();
                else
                { 
                    bool frogBehindAttachedWave = AttachedWave.transform.position.x > frog.transform.position.x;
                    if (frogBehindAttachedWave) AttachClosestWave();
                }
                
            }
        }

        public void OnLeftPlatform() => AttachClosestWave();

        void AttachClosestWave()
        {
            if (AttachedWave != null) AttachedWave.breakControlls.FrogTriggerBreak();

            float frogPosX = frog.transform.position.x;
            AttachedWave = waveMediator.ClosestWaveBehindPosition(frogPosX);

            if (AttachedWave == null)
            {
                AttachedWave = waveManager.GetInactiveWave();
                AttachedWave.StartWave();
            }
        }

        public void OnDeath()
        {
            if (AttachedWave == null) return;

            float resetPos = frog.transform.position.x - GM.respawnSetBack;
            bool frogResetsBehindWave = resetPos < AttachedWave.transform.position.x;
            //bool frogResetsOntoPlatform = resetPos < frog.currentLevel.startLength;

            if (frogResetsBehindWave)
                AttachedWave.breakControlls.FrogTriggerBreak();
        }
        public void OnEndLevel() => AttachedWave.breakControlls.FrogTriggerBreak();
        public void OnRestart()
        {
            if (AttachedWave == null) return;
            AttachedWave.breakControlls.FrogTriggerBreak();
        }
    }
}