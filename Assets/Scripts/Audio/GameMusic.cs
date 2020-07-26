using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour, IRespawnResetable
{

    AudioClip detuneMusic;
    AudioClip regularMusic;


    //DETUNE
    float detuneDurationSeconds = 1;
    float detuneTimer = 1000;
    //DETUNE


    //BEATFRAME
    readonly int bpm = 250;
    float secondsPerBeat;
    public bool IsBeatFrame { get; private set; } = false;
    float secondsSinceLastBeatFrame;
    //BEATFRAME


    //MUSIC ZONE
    List<MusicZone> musicZones = new List<MusicZone>();
    int currentMusicZone = 0;
    bool waitingToSwitchZone = false;
    int zoneToSwitchTo;
    int zonePlayerDiedIn;
    //MUSIC ZONE


    private void Awake()
    {
        GM.gameMusic = this;
        GM.AddRespawnResetable(this);
        secondsPerBeat = 60 / bpm;
        secondsSinceLastBeatFrame = secondsPerBeat + 1;
    }

    void Start()
    {
        regularMusic = GM.audioManager.GetAudioClip("BackgroundMusic");
        detuneMusic = GM.audioManager.GetAudioClip("DetuneMusic");
        //detuneMusic.audioSource.Play();
        //detuneMusic.audioSource.volume = 0;

        //MUSIC ZONE
        musicZones[0].PlayZone();
        //MUSIC ZONE
    }

    void FixedUpdate()
    {

        //DETUNE
        if(detuneTimer < detuneDurationSeconds) //timer is counting
        {
            detuneTimer += Time.deltaTime;
            if(detuneTimer >= detuneDurationSeconds) //time is up, stop the detune
            {
                //detuneMusic.audioSource.volume = 0;
                //regularMusic.audioSource.volume = regularMusic.MaxVolume;
                musicZones[currentMusicZone].TuneZone();
            }
        }
        //DETUNE


        //BEATFRAME
        secondsSinceLastBeatFrame += Time.deltaTime;
        IsBeatFrame = false;
        if(secondsSinceLastBeatFrame >= secondsPerBeat) { IsBeatFrame = true; }
        //BEATFRAME


        //MUSIC ZONE
        if ((currentMusicZone < musicZones.Count - 1)){
            if (musicZones[currentMusicZone + 1].IsPlayerPastZoneStart()) //player moved into the next zone
            {
                waitingToSwitchZone = true;
                zoneToSwitchTo = currentMusicZone + 1;
                musicZones[currentMusicZone].StopLooping();
            }
        }

        if (waitingToSwitchZone) //the next zone will start playing soon
        {
            if (musicZones[currentMusicZone].HasFinishedCurrentLoop()) //ready to switch over
            {
                Debug.Log("switching");
                musicZones[currentMusicZone].StopPlayingZone();
                currentMusicZone++;
                musicZones[currentMusicZone].PlayZone();
                waitingToSwitchZone = false;
            }
        }
        //MUSIC ZONE
    }

    public void DetuneMusic()
    {
        //detuneMusic.audioSource.volume = detuneMusic.MaxVolume;
        //regularMusic.audioSource.volume = 0;
        detuneTimer = 0;
        musicZones[currentMusicZone].DetuneZone();



    }

    public void RespawnReset()
    {
        //Loop through all zones to check which zone your in now, since you died and got sent backwards
        zonePlayerDiedIn = currentMusicZone;
        currentMusicZone = -1; //should always be past the first zone
        foreach (MusicZone zone in musicZones)
        {
            if (zone.IsPlayerPastZoneStart())
            {
                currentMusicZone++;
                //Debug.Log("CURRENT ZONE: " + currentMusicZone);
            }
        }

        //player is in a different zone now
        if (zonePlayerDiedIn != currentMusicZone)
        {
            //Debug.Log(currentMusicZone);
            musicZones[zonePlayerDiedIn].StopPlayingZone();
            musicZones[currentMusicZone].PlayZone();
        }
    }

    public void GoToMusicZone()
    {

    }

    public void AddMusicZone(MusicZone zone)
    {
        musicZones.Add(zone);
        foreach(MusicZone i in musicZones)
        {
            Debug.Log(i);
        }
    }
}
