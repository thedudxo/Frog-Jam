using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GM {

    public delegate void VoidBoolDelg(bool b);

    public static ProgressBar progressBar; //referenced by analitics when quit game, should probably be moved
    public static LevelScripts.Level currentLevel;
    public static SplitManager splitManager;
    public static AudioManager audioManager;
    public static GameMusic gameMusic;

    static List<IRespawnResetable> resetOnRespawn = new List<IRespawnResetable>();
    static List<ILevelRestartResetable> resetOnLevelRestart = new List<ILevelRestartResetable>();
    static List<IDeathResetable> resetOnDeath = new List<IDeathResetable>();

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





    // Various methods called to objects when respawning/restarting/dying
    public static void AddRespawnResetable(IRespawnResetable resetable)
        { resetOnRespawn.Add(resetable); }

    public static void AddLevelRestartResetable(ILevelRestartResetable resetable)
        { resetOnLevelRestart.Add(resetable); }

    public static void AddDeathResetable(IDeathResetable resetable)
    { resetOnDeath.Add(resetable);}

    public static void RemoveDeathResetable(IDeathResetable resetable)
    {
        resetOnDeath.Remove(resetable);
    }


    static public void PhillRespawned()
    {
        foreach (IRespawnResetable resetable in resetOnRespawn)
            { resetable.PhillRespawned(); }
        CurrentRespawnCount++;
    }

    public static void LevelRestart()
    {
        foreach (ILevelRestartResetable resetable in resetOnLevelRestart)
        { resetable.PhillRestartedLevel(); }
    }

    public static void PhillDied()
    {
        foreach (IDeathResetable resetable in resetOnDeath)
        { resetable.PhillDied(); }
    }


    public static void QuitToMenu()
    {
        SceneManager.LoadScene(0);
        resetOnRespawn = new List<IRespawnResetable>(); //reset or else the list will grow
    }

}
