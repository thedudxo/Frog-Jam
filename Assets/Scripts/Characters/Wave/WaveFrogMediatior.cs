using FrogScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WaveScripts;

public class WaveFrogMediatior : MonoBehaviour
{
    [SerializeField] WaveCollection waveManager;
    [SerializeField] FrogManager frogManager;


    public Wave ClosestWaveBehindPosition(float pos) => waveManager.ClosestBehind(pos);

}