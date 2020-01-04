using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Slider playerProgressBar;
    [SerializeField] Slider waveProgressBar;
    [SerializeField] Slider personalBest;
    [SerializeField] Slider progressLost;
    [SerializeField] Level level;
    [SerializeField] float progressLostDecaySpeed;

    private void Start()
    {
        GM.progressBar = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerProgressBar.value = (FrogManager.frog.transform.position.x - level.startX) / (level.endX - level.startX);
        waveProgressBar.value   = (level.wave.transform.position.x   - level.startX) / (level.endX - level.startX);

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

    public void PhillDied()
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
}
