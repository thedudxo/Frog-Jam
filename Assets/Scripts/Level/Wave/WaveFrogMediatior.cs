using System.Collections;
using UnityEngine;
using waveScripts;
using FrogScripts;


public class WaveFrogMediatior : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] FrogManager frogManager;

    public void FrogLeftStartPlatform()
    {
        waveManager.ReleaseWave();
    }
}