using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{


    [SerializeField] Slider playerProgressBar;
    [SerializeField] Slider WaveProgressBar;
    [SerializeField] Level level;
    [SerializeField] GameObject winScreen;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerProgressBar.value = (level.player.transform.position.x - level.startX) / (level.endX - level.startX);
        WaveProgressBar.value   = (level.wave.transform.position.x   - level.startX) / (level.endX - level.startX);

        if(playerProgressBar.value >= 1)
        {
            winScreen.SetActive(true);
        }
    }
}
