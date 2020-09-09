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
    public readonly double musicStartDelay = 5;

    //BEATFRAME
    float secondsPerBeat;
    public bool IsBeatFrame { get; private set; } = false;
    float secondsSinceLastBeatFrame;

    //MUSIC ZONE
    List<MusicZone> musicZones = new List<MusicZone>();
    int currentZoneIndex = 0;
    int zonePlayerDiedIn;

    double musicTime = 0;
    double dspStartTime = 0;

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
        dspStartTime = AudioSettings.dspTime;

        //MUSIC ZONE
        musicZones[0].PlayZone(AudioSettings.dspTime + musicStartDelay);
        //MUSIC ZONE

    }

    void Update()
    {

        musicTime = (AudioSettings.dspTime - dspStartTime) - musicStartDelay;

        //DETUNE
        if(detuneTimer < detuneDurationSeconds) //timer is counting
        {
            detuneTimer += Time.deltaTime;
            if(detuneTimer >= detuneDurationSeconds) //time is up, stop the detune
            {
                //detuneMusic.audioSource.volume = 0;
                //regularMusic.audioSource.volume = regularMusic.MaxVolume;
                musicZones[currentZoneIndex].TuneZone();
            }
        }

        //BEATFRAME
        secondsSinceLastBeatFrame += Time.deltaTime;
        IsBeatFrame = false;
        if(secondsSinceLastBeatFrame >= secondsPerBeat) { IsBeatFrame = true; }


        //MUSIC ZONE
        MusicZone currentZone = musicZones[currentZoneIndex]; //bit weird since it allready exists

        //get the time untill the current bar ends
        double timeThroughCurrentBar = currentZone.TimeElapsed() % barLength;
        double timeToNextBar = barLength - timeThroughCurrentBar;

        //get the time untill the current beat ends
        double timeThroughCurrentBeat = currentZone.TimeElapsed() % beatLength;
        double timeToNextBeat = beatLength - timeThroughCurrentBeat;

        double timeToNextSwitch = timeToNextBar; //use to change if bars or beats gets used

        if ((currentZoneIndex < musicZones.Count - 1)){ //there is actually another zone to move into
            if (musicZones[currentZoneIndex + 1].IsPlayerPastZoneStart()) //player moved into the next zone
            {
                currentZoneIndex++;
                MusicZone nextZone = musicZones[currentZoneIndex];

                double playPositionAtEndBar = currentZone.TimeElapsed() + timeToNextSwitch;

                //switch which zone is playing
                currentZone.StopPlayingZone(timeToNextSwitch + musicTime + dspStartTime + musicStartDelay);
                nextZone.PlayZone(timeToNextSwitch + musicTime + dspStartTime + musicStartDelay);
                //Debug.Log("playing next zone in: " + timeToNextSwitch);

                //set the play position of the next music zone to be the same as when the current one ends
                nextZone.SetPlayPosition(
                    (musicTime + timeToNextSwitch) % clipLength); 
                    //cant use dspStartTime and %cliplength in the same eq
            }
        }

        //DEBUG UI
        dspTimeText.text = (musicTime).ToString("F3");
        currentBarSlider.value = (float) (timeThroughCurrentBar /  barLength);
        currentBeatSlider.value = (float)(timeThroughCurrentBeat / beatLength);
        currentZoneText.text = "" + currentZoneIndex;

        float currentZonePosX = musicZones[currentZoneIndex].gameObject.transform.position.x;
        float nextZonePosX = musicZones[Mathf.Clamp(currentZoneIndex + 1, 0, musicZones.Count - 1)].gameObject.transform.position.x;
        float playerPosX = FrogManager.frog.transform.position.x;
        float distanceBetweenZones = nextZonePosX - currentZonePosX;
        float progressThroughZone = playerPosX - currentZonePosX;
        float progressNormalised = progressThroughZone / distanceBetweenZones;
        nextZoneInSlider.value = progressNormalised;

        currentClipSlider.value = currentZone.PlayPositionNormalised;

    }

    public void DetuneMusic()
    {
        //detuneMusic.audioSource.volume = detuneMusic.MaxVolume;
        //regularMusic.audioSource.volume = 0;
        detuneTimer = 0;
        musicZones[currentZoneIndex].DetuneZone();



    }

    public void RespawnReset()
    {
        //Loop through all zones to check which zone your in now, since you died and got sent backwards
        zonePlayerDiedIn = currentZoneIndex;
        currentZoneIndex = -1; //should always be past the first zone
        foreach (MusicZone zone in musicZones)
        {
            if (zone.IsPlayerPastZoneStart())
            {
                currentZoneIndex++;
                //Debug.Log("CURRENT ZONE: " + currentMusicZone);
            }
        }

        //player is in a different zone now
        if (zonePlayerDiedIn != currentZoneIndex)
        {
            //get the time untill the current bar ends
            double timeRemaningToEndOfBar = musicZones[currentZoneIndex].TimeElapsed() % barLength;
            double timeToNextBar = barLength - timeRemaningToEndOfBar;


            musicZones[zonePlayerDiedIn].StopPlayingZone(timeRemaningToEndOfBar + musicTime);
            musicZones[currentZoneIndex].PlayZone(timeRemaningToEndOfBar + musicTime);
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
