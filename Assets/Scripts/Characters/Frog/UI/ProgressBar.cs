using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] Slider waveProgressBar;

    [SerializeField] LevelScripts.Level level;

    private void Start()
    {
        GM.progressBar = this; //referenced by analitics when quit game, should probably be moved
    }

    void Update()
    {
        waveProgressBar.value   = (level.wave.transform.position.x   - level.startLength) / (level.end - level.startLength);
    }


}
