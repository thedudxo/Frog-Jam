using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    [SerializeField] public float waveStartSpeed;
    //[SerializeField] private float waveAcceleration; 
    //Waves really dont work this way but whatever, Maybie this wave has rocket thrusters on the back of it.


    //music
    AudioClip waveApproachingMusic;
    AudioClip ambientBackgroundMusic;
    [SerializeField] float waveApproachingMusicMaxDistance = 5;
    bool waveIsClose = false;

    Vector2 spawnPosition;
    public float waveCurrentSpeed;

    // Use this for initialization
    void Start () {
        spawnPosition = transform.position;
        waveCurrentSpeed = waveStartSpeed;

        waveApproachingMusic   = GM.audioManager.GetAudioClip("WaveApproaching");
        waveApproachingMusic.audioSource.Play();
        waveApproachingMusic.audioSource.volume = 0;
        ambientBackgroundMusic = GM.audioManager.GetAudioClip("BackgroundMusic");

    }

    private void Update()
    {
        //manage wave approach music
        float MusicVolumeNormalised  = ambientBackgroundMusic.audioSource.volume / ambientBackgroundMusic.MaxVolume;
        float WaveVolumeNormalised   = waveApproachingMusic  .audioSource.volume / waveApproachingMusic  .MaxVolume;
        float waveDistanceNormalised = 
            (FrogManager.frog.transform.position.x - transform.position.x) / waveApproachingMusicMaxDistance;
            // distance between the frog and the wave, normalised to the approach music distance
        //Debug.Log(waveDistanceNormalised);


        if (waveDistanceNormalised <= 1)
        {
            if (!waveIsClose) //first time the wave is close
            {
                waveIsClose = true;
                //ambientBackgroundMusic.audioSource.Pause();
                waveApproachingMusic.audioSource.volume = 1;
            }
            else //wave has been close for a while
            {

            }
        }
        else if (waveIsClose) //wave isnt close anymore
        {
            waveIsClose = false;
            //ambientBackgroundMusic.audioSource.UnPause();
            waveApproachingMusic.audioSource.volume = 0;
        }


    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + waveCurrentSpeed, transform.position.y);
        //waveCurrentSpeed += waveAcceleration;
    }

    public void ResetWave()
    {
        waveCurrentSpeed = waveStartSpeed;
        transform.position = spawnPosition;
    }
}
