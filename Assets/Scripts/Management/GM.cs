using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GM {

    public delegate void VoidBoolDelg(bool b);

    public static ProgressBar progressBar; //referenced by analitics when quit game, should probably be moved
    public static LevelScripts.Level currentLevel;
    public static AudioManager audioManager;
    public static GameMusic gameMusic;

    public static readonly float CameraVeiwRangeApprox = 18;
    public static int CurrentRespawnCount { get; private set; } = 1;
    public static bool sendAnyalitics = true;

    public const string playerTag = "Phill";
    public const string enemyAligator = "EnemyAligator";

    public enum GameState
    {
        playingLevel,
        finishedLevel
    }

    public static GameState gameState = GameState.playingLevel;


    public static void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        //resetOnRespawn = new List<IRespawnResetable>(); //reset or else the list will grow
    }

}
