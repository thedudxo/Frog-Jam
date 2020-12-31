using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour 
{
    //marks a spot where the next bit of music should be queued up to start playing

    [SerializeField]  UnityEngine.AudioClip normalClip, detuneClip, waveClip; //clips
    AudioSource normalAudioSource, detuneAudioSource, waveAudioSource;        //sources
    [SerializeField] int position;                                            //which number zone this is
    double ZoneStartDspTime;                                                  //dsp time this started playing at
    int sampleRate;                                                           //sample rate of all the clips (assuming they're all the same format)
    static float waveMusicDistance = 30;                                      //distance from the player before the wave starts playing music
    static float bufferInFrontOfWave = 10;                                    //distance infront of the wave where the music will be max volume

    float playPositionNormalised;                                             //between 0 to 1, the audio position the clip is currently playing
    public float PlayPositionNormalised
    {
        get {
            playPositionNormalised =  normalAudioSource.time / normalClip.length;
            return playPositionNormalised; }
            }



    float normalAudioMaxVolume = 1, detuneAudioMaxVolume = 1; // = Gamemusic.Volume

    private void Awake()
    {
        normalAudioSource = (AudioSource) gameObject.AddComponent(typeof(AudioSource));
        detuneAudioSource = (AudioSource) gameObject.AddComponent(typeof(AudioSource));
        waveAudioSource   = (AudioSource) gameObject.AddComponent(typeof(AudioSource));

        normalAudioSource.clip = normalClip;
        detuneAudioSource.clip = detuneClip;
        waveAudioSource  .clip = waveClip;

        normalAudioSource.playOnAwake = false;
        detuneAudioSource.playOnAwake = false;
        waveAudioSource.playOnAwake = false;

        normalAudioSource.Pause();
        detuneAudioSource.Pause();
        waveAudioSource.Pause();

        waveAudioSource.volume = 0;

        sampleRate = normalClip.frequency;

    }

    private void Start()
    {
        GM.gameMusic.AddMusicZone(this, position);
    }

    public bool IsPlayerPastZoneStart()
    {
        if (SingletonThatNeedsToBeRemoved.frog.transform.position.x > transform.position.x)
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
        waveAudioSource.PlayScheduled(dspTime);

        SetLooping(true);

        TuneZone();

        ZoneStartDspTime = dspTime;
    }

    public void StopPlayingZone(double dspTime)
    {
        normalAudioSource.SetScheduledEndTime(dspTime);
        detuneAudioSource.SetScheduledEndTime(dspTime);
        waveAudioSource.SetScheduledEndTime(dspTime);
        SetLooping(false);
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


    public void SetLooping(bool loop)
    {
        normalAudioSource.loop = loop;
        detuneAudioSource.loop = loop;
        waveAudioSource  .loop = loop;
    }

    public double TimeElapsed()
    {
        return AudioSettings.dspTime - ZoneStartDspTime;
    }

    public void SetPlayPosition(double position)
    {
        //Debug.Log("seeking to: " + position);
        int sample = System.Convert.ToInt32 (position * sampleRate);
        normalAudioSource.timeSamples = sample;
        detuneAudioSource.timeSamples = sample;
        waveAudioSource.timeSamples = sample;
    }

    public void UpdateWaveAudio()
    {

        float wavePos = GM.currentLevel.wave.transform.position.x;
        float frogPos = SingletonThatNeedsToBeRemoved.frog.transform.position.x;

        float distanceNormalised = 1 - Mathf.Clamp01((frogPos - (wavePos + bufferInFrontOfWave)) / (waveMusicDistance));
        waveAudioSource.volume = distanceNormalised;
    }
}