using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GM {

    public static ProgressBar progressBar;
    public static ComboCounter comboCounter;
    public static FrogDeath frogDeath;
    public static Level currentLevel;
    public static SplitManager splitManager;
    public static AudioManager audioManager;


    static List<GameObject> gaters = new List<GameObject>();

    public static readonly string playerTag = "Phill";

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
