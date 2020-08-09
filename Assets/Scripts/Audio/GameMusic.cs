using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMusic : MonoBehaviour, IRespawnResetable
{

    AudioClip detuneMusic;
    AudioClip regularMusic;

    //DETUNE
    float detuneDurationSeconds = 1;
    float detuneTimer = 1000;

    //Beatmatching
    readonly int bpm = 250;
    double beatLength, barLength, clipLength;
    public readonly double dspStartTime = 1;

    //BEATFRAME
    float secondsPerBeat;
    public bool IsBeatFrame { get; private set; } = false;
    float secondsSinceLastBeatFrame;

    //MUSIC ZONE
    List<MusicZone> musicZones = new List<MusicZone>();
    int currentMusicZone = 0;
    int zonePlayerDiedIn;

    //DEBUG MENU
    [SerializeField] Slider currentClipSlider, currentBarSlider, currentBeatSlider, nextZoneInSlider;
    [SerializeField] Text dspTimeText, currentZoneText;

    private void Awake()
    {
        GM.gameMusic = this;
        GM.AddRespawnResetable(this);


        //Beatmatching
        beatLength = 60d / bpm; //d specifies double
        barLength = beatLength * 4d;
        clipLength = barLength * 8;

        //beatframes
        secondsPerBeat = 60 / bpm;
        secondsSinceLastBeatFrame = secondsPerBeat + 1;
    }

    void Start()
    {
        //MUSIC ZONE
        musicZones[0].PlayZone(AudioSettings.dspTime + dspStartTime);
        //MUSIC ZONE
    }

    void Update()
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


        //Beatmatching
        double musicTime = AudioSettings.dspTime - dspStartTime;


        //BEATFRAME
        secondsSinceLastBeatFrame += Time.deltaTime;
        IsBeatFrame = false;
        if(secondsSinceLastBeatFrame >= secondsPerBeat) { IsBeatFrame = true; }


        //MUSIC ZONE
        MusicZone currentZone = musicZones[currentMusicZone]; //bit weird since it allready exists

        //get the time untill the current bar ends
        double timeThroughCurrentBar = currentZone.TimeElapsed() % barLength;
        double timeToNextBar = barLength - timeThroughCurrentBar;

        //get the time untill the current beat ends
        double timeThroughCurrentBeat = currentZone.TimeElapsed() % beatLength;
        double timeToNextBeat = beatLength - timeThroughCurrentBeat;

        if ((currentMusicZone < musicZones.Count - 1)){
            if (musicZones[currentMusicZone + 1].IsPlayerPastZoneStart()) //player moved into the next zone
            {
                //Debug.Log("switching");
                
                currentMusicZone++;
                MusicZone nextZone = musicZones[currentMusicZone];

               


                double playPositionAtEndBar = currentZone.TimeElapsed() + timeToNextBar;
                //Debug.Log(musicZones[currentMusicZone].TimeElapsed());

                currentZone.StopPlayingZone(timeToNextBar + AudioSettings.dspTime);

                nextZone.PlayZone(timeToNextBar + AudioSettings.dspTime);
                //set the play position of the next music zone to be the same as when the current one ends
                nextZone.SetPlayPosition(
                    (AudioSettings.dspTime - dspStartTime + timeToNextBar) % clipLength);
            }
        }

        //DEBUG UI
        dspTimeText.text = (AudioSettings.dspTime - dspStartTime).ToString("F3");
        currentBarSlider.value = (float) (timeThroughCurrentBar /  barLength);
        currentBeatSlider.value = (float)(timeThroughCurrentBeat / beatLength);
        currentZoneText.text = "" + currentMusicZone;

        //next zone slider
        float currentZonePosX = musicZones[currentMusicZone].gameObject.transform.position.x;
        float nextZonePosX = musicZones[Mathf.Clamp(currentMusicZone + 1, 0, musicZones.Count - 1)].gameObject.transform.position.x;
        float playerPosX = FrogManager.frog.transform.position.x;
        float distanceBetweenZones = nextZonePosX - currentZonePosX;
        float progressThroughZone = playerPosX - currentZonePosX;
        float progressNormalised = progressThroughZone / distanceBetweenZones;
        nextZoneInSlider.value = progressNormalised;


        //current clip slider
        //currentZone.
        //currentClipSlider.value = 

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
            //get the time untill the current bar ends
            double timeRemaningToEndOfBar = musicZones[currentMusicZone].TimeElapsed() % barLength;
            double timeToNextBar = barLength - timeRemaningToEndOfBar;


            musicZones[zonePlayerDiedIn].StopPlayingZone(timeRemaningToEndOfBar + AudioSettings.dspTime);
            musicZones[currentMusicZone].PlayZone(timeRemaningToEndOfBar + AudioSettings.dspTime);
        }
    }

    public void GoToMusicZone()
    {

    }

    public void AddMusicZone(MusicZone zone)
    {
        musicZones.Add(zone);
    }
}
