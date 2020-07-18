using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClip
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public string name;

    public float MaxVolume { get; private set; }


    public AudioClip(string name)
    {
        this.name = name;
    }

    public void Startup()
    {
        MaxVolume = audioSource.volume;
    }

}
