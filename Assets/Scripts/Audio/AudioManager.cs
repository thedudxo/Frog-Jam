using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float effectsVolume; //does nothing Right now

    [Header("FrogSounds")]
    [SerializeField] List<AudioClip> audioSources;


    void Awake()
    {
        GM.audioManager = this;
        foreach (AudioClip clip in audioSources)
        {
            clip.Startup();
        }
    }

    private void Start()
    {

    }

    private AudioClip SearchForClip(string name)
    {
        Debug.Log("Searching " + audioSources.Count + " audioclips for " + name);

        foreach (AudioClip clip in audioSources)
        {
            if (clip.name == name) { return clip; }
        }

        throw new System.Exception("Could not find Audioclip: " + name);
    }

    
    //public void PlaySound(string name)
    //{  SearchForClip(name).audioSource.Play(); }

    //public void PauseSound(string name)
    //{  SearchForClip(name).audioSource.Pause(); }

    //public void UnPauseSound(string name)
    //{  SearchForClip(name).audioSource.UnPause();}

    public AudioClip GetAudioClip(string name)
    {
        return SearchForClip(name);
    }
}
