using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GM {

    public static ProgressBar progressBar;
    public static ComboCounter comboCounter;
    public static Level currentLevel;
    public static SplitManager splitManager;
    public static AudioManager audioManager;

    static List<GameObject> gaters = new List<GameObject>();

    public static readonly string playerTag = "Phill";
    public static bool sendAnyalitics = true;

    public enum GameState
    {
        alive,
        dead,
        finishedLevel
    }

    public static GameState gameState = GameState.alive;



    static public void PhillRespawned()
    {
        foreach (GameObject gater in gaters)
        {
            gater.GetComponent<AlliA>().ResetGater();
        }
        progressBar.PhillDied();
    }

    static public void PhillDied()
    {
        comboCounter.CheckCombo();
    }

    public static void AddGater(GameObject gater)
    {
        gaters.Add(gater);
    }

}
