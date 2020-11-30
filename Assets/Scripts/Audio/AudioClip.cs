using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioClip : MonoBehaviour
{
    [SerializeField] public List<AudioSource> audioSources;
    [SerializeField] public string audioName;


    public void Start()
    {
        GM.audioManager.AddAudioClip(this);
    }

    public AudioSource GetRandomAudioSource()
    {
        return audioSources[Random.Range(0, audioSources.Count - 1)];
    }

}
