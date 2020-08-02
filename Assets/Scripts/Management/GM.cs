using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GM {

    public static ProgressBar progressBar; //referenced by analitics when quit game, should probably be moved
    public static ComboCounter comboCounter;
    public static Level currentLevel;
    public static SplitManager splitManager;
    public static AudioManager audioManager;
    public static GameMusic gameMusic;

    static List<GameObject> gaters = new List<GameObject>();
    static List<IRespawnResetable> resetOnRespawn = new List<IRespawnResetable>();


    public static readonly float CameraVeiwRangeApprox = 18;
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
        foreach(IRespawnResetable resetable in resetOnRespawn)
        {
            resetable.RespawnReset();
        }

        CurrentRespawnCount++;
    }

    static public void PhillDied()
    {
        comboCounter.CheckCombo();
    }

    //these have a method called whenever phill RESPAWNS
    public static void AddRespawnResetable(IRespawnResetable resetable)
    {
        resetOnRespawn.Add(resetable);
    }

}
