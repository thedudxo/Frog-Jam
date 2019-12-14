using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GM {

    public static ProgressBar progressBar;
    public static ComboCounter comboCounter;

    static List<GameObject> gaters = new List<GameObject>();



    static public void PhillDied()
    {
     foreach(GameObject gater in gaters)
        {
            gater.GetComponent<AlliA>().ResetGater();
        }

        progressBar.checkPersonalBest();
        comboCounter.CheckCombo();
    }

    public static void AddGater(GameObject gater)
    {
        gaters.Add(gater);
    }
}
