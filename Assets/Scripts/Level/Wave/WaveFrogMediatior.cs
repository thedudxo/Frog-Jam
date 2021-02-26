using System.Collections;
using UnityEngine;
using waveScripts;
using FrogScripts;


public class WaveFrogMediatior : MonoBehaviour, INotifyAnyFrogLeftPlatform
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] FrogManager frogManager;

    private void Start()
    {
        frogManager.events.SubscribeAnyFrogLeftPlatform(this);
    }

    public void AnyFrogLeftPlatform()
    {
        waveManager.ReleaseWave();
    }
}