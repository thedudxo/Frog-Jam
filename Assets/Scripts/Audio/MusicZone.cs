using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
    //marks a spot where the next bit of music should be queued up to start playing

    [SerializeField] UnityEngine.AudioClip normalClip, detuneClip, waveClip;
    AudioSource normalAudioSource, detuneAudioSource, waveAudioSource;


    float normalAudioMaxVolume = 1, detuneAudioMaxVolume = 1; // = Gamemusic.Volume

    double dspStartTime;
    int sampleRate;

    private void Awake()
    {
        normalAudioSource = (AudioSource) gameObject.AddComponent(typeof(AudioSource));
        detuneAudioSource = (AudioSource) gameObject.AddComponent(typeof(AudioSource));
        waveAudioSource   = (AudioSource) gameObject.AddComponent(typeof(AudioSource));

        normalAudioSource.clip = normalClip;
        detuneAudioSource.clip = detuneClip;
        waveAudioSource  .clip = waveClip;

        sampleRate = normalClip.frequency;

    }

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

    public void PlayZone(double dspTime)
    {
        //Debug.Log("playing in: " + (dspTime - AudioSettings.dspTime));
        normalAudioSource.PlayScheduled(dspTime);
        detuneAudioSource.PlayScheduled(dspTime);

        normalAudioSource.loop = true;
        detuneAudioSource.loop = true;

        TuneZone();

        dspStartTime = dspTime;
    }

    public void StopPlayingZone(double dspTime)
    {
        normalAudioSource.SetScheduledEndTime(dspTime);
        detuneAudioSource.SetScheduledEndTime(dspTime);
        StopLooping();
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

    public double TimeElapsed()
    {
        return AudioSettings.dspTime - dspStartTime;
    }

    public void SetPlayPosition(double position)
    {
        //Debug.Log("seeking to: " + position);
        int sample = System.Convert.ToInt32 (position * sampleRate);
        normalAudioSource.timeSamples = sample;
        detuneAudioSource.timeSamples = sample;
        waveAudioSource.timeSamples = sample;
    }
}