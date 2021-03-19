using FrogScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WaveScripts;

public class WaveFrogMediatior : MonoBehaviour, INotifyAnyFrogLeftPlatform
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] FrogManager frogManager;

    private void Start()
    {
        frogManager.events.SubscribeAnyFrogLeftPlatform(this); 
    }

    public void AnyFrogLeftPlatform() => waveManager.waveStarter.StartWave();

    public Wave ClosestWaveBehindPosition(float pos) => waveManager.ClosestWaveBehindPosition(pos);

    public Frog GetLastFrog() => frogManager.GetLastFrog();

    public bool NoWaveBehindLastFrog()
    {
        float lastFrogPosX = GetLastFrog().transform.position.x;

        bool noWaveBehindLastFrog = true;

        foreach (Wave wave in waveManager.waves)
        {
            if (wave.state == Wave.State.normal)
                if (lastFrogPosX > wave.transform.position.x)
                {
                    noWaveBehindLastFrog = false;
                    break;
                }
        }

        return noWaveBehindLastFrog;
    }
}