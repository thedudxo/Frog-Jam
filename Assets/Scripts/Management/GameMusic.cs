using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{

    AudioClip detuneMusic;
    AudioClip regularMusic;

    float detuneDurationSeconds = 1;
    float detuneTimer = 1000;

    readonly int bpm = 250;
    float secondsPerBeat;
    public bool isBeatFrame { get; private set; } = false;
    float secondsSinceLastBeatFrame;

    private void Awake()
    {
        GM.gameMusic = this;
        secondsPerBeat = 60 / bpm;
        secondsSinceLastBeatFrame = secondsPerBeat + 1;
    }

    void Start()
    {
        regularMusic = GM.audioManager.GetAudioClip("BackgroundMusic");
        detuneMusic = GM.audioManager.GetAudioClip("DetuneMusic");
        detuneMusic.audioSource.Play();
        detuneMusic.audioSource.volume = 0;
    }

    void Update()
    {

        //DETUNE
        if(detuneTimer < detuneDurationSeconds) //timer is counting
        {
            detuneTimer += Time.deltaTime;
            if(detuneTimer >= detuneDurationSeconds) //time is up
            {
                detuneMusic.audioSource.volume = 0;
                regularMusic.audioSource.volume = regularMusic.MaxVolume;
            }
        }
        //DETUNE


        //BEATFRAME
        secondsSinceLastBeatFrame += Time.deltaTime;
        isBeatFrame = false;
        if(secondsSinceLastBeatFrame >= secondsPerBeat) { isBeatFrame = true; }
        //BEATFRAME
    }

    public void DetuneMusic()
    {
        detuneMusic.audioSource.volume = detuneMusic.MaxVolume;
        Debug.Log(detuneMusic.audioSource.volume);
        regularMusic.audioSource.volume = 0;
        detuneTimer = 0;
    }
}
