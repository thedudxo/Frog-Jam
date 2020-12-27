using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IRespawnResetable
{
    [SerializeField] Slider playerProgressBar;
    [SerializeField] Slider waveProgressBar;
    [SerializeField] Slider personalBest;
    [SerializeField] Slider progressLost;
    [SerializeField] Level.Level level;
    [SerializeField] float progressLostDecaySpeed;

    private void Start()
    {
        GM.progressBar = this; //referenced by analitics when quit game, should probably be moved
        GM.AddRespawnResetable(this);
    }

    // Update is called once per frame
    void Update()
    {
        playerProgressBar.value = (FrogManager.frog.transform.position.x - level.startLength) / (level.end - level.startLength);
        waveProgressBar.value   = (level.wave.transform.position.x   - level.startLength) / (level.end - level.startLength);

        //update looseProgressBar
        if (progressLost.gameObject.activeInHierarchy)
        {
            progressLost.value -= progressLostDecaySpeed;

            if(progressLost.value <= playerProgressBar.value)
            {
                progressLost.gameObject.SetActive(false);
            }
        }
    }

    public void PhillRespawned()
    {
        checkPersonalBest();
        looseProgress();
    }

    void checkPersonalBest()
    {
        if (! personalBest.gameObject.activeInHierarchy)
        {
            personalBest.gameObject.SetActive(true);
            
        }

        if(personalBest.value < playerProgressBar.value)
        {
            personalBest.value = playerProgressBar.value;
        }
    }

    void looseProgress()
    {
        progressLost.gameObject.SetActive(true);
        progressLost.value = playerProgressBar.value;
    }

    public float GetPersonalBest()
    {
        return personalBest.value;
    }
}
