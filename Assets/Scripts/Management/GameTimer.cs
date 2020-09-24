using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    static GameTimer gameTimer;

    private void Start()
    {
        if(gameTimer = null)
        {
            gameTimer = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        Statistics.timeInSession += Time.deltaTime; //TODO: make another that tracks per level
        
    }
}
