using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMusic : MonoBehaviour, IRespawnResetable
{
    //DETUNE
    float detuneDurationSeconds = 1;
    float detuneTimer = 1000;

    //Beatmatching
    readonly int bpm = 250;
    double beatLength, barLength, clipLength;
    public readonly double musicStartDelay = 1;

    //BEATFRAME
    float secondsPerBeat;
    public bool IsBeatFrame { get; private set; } = false;
    float secondsSinceLastBeatFrame;

    //MUSIC ZONE
    [SerializeField] GameObject musicZonesParent;
    MusicZone[] musicZones;
    MusicZone currentZone;
    int currentZoneIndex = 0;
    int zonePlayerDiedIn;

    double musicTime, dspStartTime, timeToNextBeat, timeToNextBar;

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

        musicZones = new MusicZone[musicZonesParent.transform.childCount];
    }

    void Start()
    {
        dspStartTime = AudioSettings.dspTime;
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
                musicZones[currentZoneIndex].TuneZone();
            }
        }

        //BEATFRAME
        secondsSinceLastBeatFrame += Time.deltaTime;
        IsBeatFrame = false;
        if(secondsSinceLastBeatFrame >= secondsPerBeat) { IsBeatFrame = true; }


        //MUSIC ZONE
        currentZone = musicZones[currentZoneIndex]; //bit weird since it allready exists

        //get the time untill the current bar ends
        double timeThroughCurrentBar = currentZone.TimeElapsed() % barLength;
        timeToNextBar = barLength - timeThroughCurrentBar;

        //get the time untill the current beat ends
        double timeThroughCurrentBeat = currentZone.TimeElapsed() % beatLength;
        timeToNextBeat = beatLength - timeThroughCurrentBeat;



        if ((currentZoneIndex < musicZones.Length - 1)){ //there is actually another zone to move into
            if (musicZones[currentZoneIndex + 1].IsPlayerPastZoneStart()) //player moved into the next zone
            {
                currentZoneIndex++;
                MusicZone nextZone = musicZones[currentZoneIndex];

                SwitchMusicZone(currentZone, nextZone, timeToNextBeat);
            }
        }

        currentZone.UpdateWaveAudio();

        //DEBUG UI
        { 
            dspTimeText.text = (musicTime).ToString("F3");
            currentBarSlider.value = (float)(timeThroughCurrentBar / barLength);
            currentBeatSlider.value = (float)(timeThroughCurrentBeat / beatLength);
            currentZoneText.text = "" + currentZoneIndex;

            float currentZonePosX = musicZones[currentZoneIndex].gameObject.transform.position.x;
            float nextZonePosX = musicZones[Mathf.Clamp(currentZoneIndex + 1, 0, musicZones.Length - 1)].gameObject.transform.position.x;
            float playerPosX = SingletonThatNeedsToBeRemoved.frog.transform.position.x;
            float distanceBetweenZones = nextZonePosX - currentZonePosX;
            float progressThroughZone = playerPosX - currentZonePosX;
            float progressNormalised = progressThroughZone / distanceBetweenZones;
            nextZoneInSlider.value = progressNormalised;

            currentClipSlider.value = currentZone.PlayPositionNormalised;
        }
    }


    public void PhillRespawned()
    {
        //Loop through all zones to check which zone your in now, since you died and got sent backwards
        zonePlayerDiedIn = currentZoneIndex;
        currentZoneIndex = -1; //should always be past the first zone
        foreach (MusicZone zone in musicZones)
        {
            if (zone.IsPlayerPastZoneStart())
            {
                currentZoneIndex++;
            }
        }

        //player is in a different zone now
        if (zonePlayerDiedIn != currentZoneIndex)
        {
            SwitchMusicZone(musicZones[zonePlayerDiedIn], musicZones[currentZoneIndex], timeToNextBeat);
        }
    }

    public void SwitchMusicZone(MusicZone currentZone, MusicZone nextZone, double timeUntillSwitch)
    {
        //switch which zone is playing
        currentZone.StopPlayingZone(timeUntillSwitch + musicTime + dspStartTime + musicStartDelay);
        nextZone.PlayZone(timeUntillSwitch + musicTime + dspStartTime + musicStartDelay);

        //set the play position of the next music zone to be the same as when the current one ends
        nextZone.SetPlayPosition((musicTime + timeUntillSwitch) % clipLength);
    }

    public void DetuneMusic()
    {
        detuneTimer = 0;
        musicZones[currentZoneIndex].DetuneZone();
    }

    public void AddMusicZone(MusicZone zone, int position)
    {
       //unity execution order is dumb so this is how i have to ensure the list is in the correct order
       //also have to start playing the first zone here again because unity execution jankyness

        musicZones[position] = zone;

        if (position == 0) //starting zone
        {
            musicZones[0].PlayZone(AudioSettings.dspTime + musicStartDelay);
        }

    }
}
