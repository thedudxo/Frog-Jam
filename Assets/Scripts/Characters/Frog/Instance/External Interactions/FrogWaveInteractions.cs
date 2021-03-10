using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using waveScripts;

namespace FrogScripts
{
    public class FrogWaveInteractions : MonoBehaviour, INotifyOnLeftPlatform, INotifyOnDeath, INotifyOnEndLevel, INotifyOnRestart
    {
        [SerializeField] public Frog frog;
        WaveManager waveManager;
        FrogManager frogManager;

        WaveFrogMediatior waveMediator;

        public Wave attachedWave;

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
            bool frogBehindAttachedWave = attachedWave?.transform.position.x > frog.transform.position.x;
            if (frogBehindAttachedWave && frog.state != FrogState.State.StartPlatform) AttachNewWave();
        }

        public void OnLeftPlatform() => AttachNewWave();

        void AttachNewWave()
        {
            if (attachedWave != null) attachedWave.breakControlls.FrogTriggerBreak();

            float frogPosX = frog.transform.position.x;
            attachedWave = waveMediator.ClosestWaveBehindPosition(frogPosX);

            if (attachedWave == null)
            {
                attachedWave = waveManager.GetInactiveWave();
                attachedWave.StartWave();
            }
        }

        public void OnDeath()
        {
            if (attachedWave == null) return;

            float resetPos = frog.transform.position.x - GM.respawnSetBack;
            bool frogResetsBehindWave = resetPos < attachedWave.transform.position.x;
            //bool frogResetsOntoPlatform = resetPos < frog.currentLevel.startLength;

            if (frogResetsBehindWave)
                attachedWave.breakControlls.FrogTriggerBreak();
        }
        public void OnEndLevel() => attachedWave.breakControlls.FrogTriggerBreak();
        public void OnRestart() => attachedWave.breakControlls.FrogTriggerBreak();
    }
}