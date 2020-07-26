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
    public static GameMusic gameMusic;

    static List<GameObject> gaters = new List<GameObject>();
    static List<IRespawnResetable> resetOnRespawn = new List<IRespawnResetable>();

    public static readonly string playerTag = "Phill";
    public static int CurrentRespawnCount { get; private set; } = 1;
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
            //CHANGE TO IRespawnRessetable <---------------------------------
            gater.GetComponent<AlliA>().ResetGater();
            //CHANGE TO IRespawnRessetable <---------------------------------
        }

        foreach(IRespawnResetable resetable in resetOnRespawn)
        {
            resetable.RespawnReset();
        }


        progressBar.PhillDied();

        //gameMusic.PhillRespawned();

        CurrentRespawnCount++;
    }

    static public void PhillDied()
    {
        comboCounter.CheckCombo();
    }

    public static void AddGater(GameObject gater)
    {
        gaters.Add(gater);
    }

    public static void AddRespawnResetable(IRespawnResetable resetable)
    {
        resetOnRespawn.Add(resetable);
    }

}
