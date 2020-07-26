using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    //marks a spot where the next bit of music should be queued up to start playing

    [SerializeField] public AudioSource normalAudioSource, detuneAudioSource;
    float normalAudioMaxVolume = 1, detuneAudioMaxVolume = 1; // = Gamemusic.Volume

    private void Start()
    {
        GM.gameMusic.AddMusicZone(this);
    }

    public bool IsPlayerPastZoneStart()
    {
        if (FrogManager.frog.transform.position.x > transform.position.x)
        {
            return true;
        }
        return false;
    }

    public void PlayZone()
    {
        normalAudioSource.Play();
        detuneAudioSource.Play();

        normalAudioSource.loop = true;
        detuneAudioSource.loop = true;

        TuneZone();

        //Debug.Log("playing Zone");
    }

    public void StopPlayingZone()
    {
        normalAudioSource.Stop();
        detuneAudioSource.Stop();
    }

    public void TuneZone()
    {
        normalAudioSource.volume = normalAudioMaxVolume;
        detuneAudioSource.volume = 0;
    }

    public void DetuneZone()
    {
        normalAudioSource.volume = 0;
        detuneAudioSource.volume = detuneAudioMaxVolume;
    }


    public void StopLooping()
    {
        Debug.Log("STOPPING LOOP");
        normalAudioSource.loop = false;
        detuneAudioSource.loop = false;
    }

    public bool HasFinishedCurrentLoop()
    {
        if (normalAudioSource.isPlaying)
        {
            return false;
        }
        return true;
    }
}
