using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveScripts;

namespace FrogScripts {
    public class DangerState : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Animator dangerAnimation;

        WaveManager waveManager;
        FrogWaveInteractions waveInteractions;

        private void Start()
        {
            waveManager = frog.currentLevel.waveManager;
            waveInteractions = frog.waveInteractions;
        }

        private void Update()
        {
            bool danger = FrogWillSetbackBehindWave(frog);
            if (frog.state == FrogState.State.StartPlatform) danger = false;

            dangerAnimation.SetBool("Danger", danger);
            frog.inDanger = danger;
        }

        bool FrogWillSetbackBehindWave(Frog frog)
        {
            Wave wave = waveInteractions.attachedWave;
            if (wave == null) return false;

            if (wave.state == Wave.State.normal)
            {
                float 
                    waveX = wave.transform.position.x, 
                    frogX = frog.transform.position.x,

                    setbackPosition = frogX - GM.respawnSetBack,
                    wavePositionAtRespawn = waveX + (WaveMovement.speed * RespawnTimer.respawnWaitSeconds);

                bool SetbackBehind = setbackPosition < (wavePositionAtRespawn + 0.05);

                if (SetbackBehind) return true;
            }

            return false;
        }
    }
}
