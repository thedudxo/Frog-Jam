using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float effectsVolume; //does nothing Right now

    [Header("FrogSounds")]
    [SerializeField] List<AudioClip> audioSources;

// Start is called before the first frame update
void Start()
    {
        GM.audioManager = this;
    }

    public void PlaySound(string name)
    {
        foreach(AudioClip clip in audioSources)
        {
            if(clip.name == name) { clip.audioSource.Play(); }
        }
    }
}
