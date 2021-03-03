using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using waveScripts;

namespace FrogScripts
{
    public class FrogWaveInteractions : MonoBehaviour    {
        [SerializeField] public Frog frog;

        WaveFrogMediatior waveMediator;

        Wave attachedWave;

        private void Start()
        {
            waveMediator = frog.currentLevel.waveFrogMediatior;
        }



    }
}