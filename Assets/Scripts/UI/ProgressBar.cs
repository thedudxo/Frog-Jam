using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{


    [SerializeField] Slider playerProgressBar;
    [SerializeField] Slider waveProgressBar;
    [SerializeField] Slider personalBest;
    [SerializeField] Level level;
    [SerializeField] GameObject winScreen;

    private void Start()
    {
        GM.progressBar = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerProgressBar.value = (level.player.transform.position.x - level.startX) / (level.endX - level.startX);
        waveProgressBar.value   = (level.wave.transform.position.x   - level.startX) / (level.endX - level.startX);

        if(playerProgressBar.value >= 1)
        {
            winScreen.SetActive(true);
        }

        if (winScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            winScreen.SetActive(false);
            level.player.GetComponent<FrogManager>().KillPhill(true);
        }
    }

    public void checkPersonalBest()
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
}
