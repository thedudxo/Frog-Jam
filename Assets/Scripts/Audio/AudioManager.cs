using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    List<AudioClip> AudioClips = new List<AudioClip>();


    void Awake()
    {
        GM.audioManager = this;
    }

    private void Start()
    {

    }
    
    public void AddAudioClip(AudioClip clip)
    {
        AudioClips.Add(clip);
    }

    public AudioClip GetAudioClip(string name)
    {
        //Debug.Log("Searching " + AudioClips.Count + " audioclips for " + name);

        foreach (AudioClip clip in AudioClips)
        {
            if (clip.audioName == name) { return clip; }
        }

        throw new System.Exception("Could not find Audioclip: " + name);
    }
}
