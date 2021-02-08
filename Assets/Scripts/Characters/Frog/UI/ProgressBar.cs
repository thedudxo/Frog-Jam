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
    }

    void Update()
    {
        //waveProgressBar.value   = (level.wave.transform.position.x   - level.startLength) / (level.end - level.startLength);
    }


}
