using System;
using UnityEngine.SceneManagement;
using static GM.PlayerMode;

public static class GM {

    public static Levels.Level currentLevel;
    public static AudioManager audioManager;
    public static GameMusic gameMusic;

    public static bool sendAnyalitics = false;

    //constants
    public const string playerTag = "Phill";
    public const string enemyAligator = "EnemyAligator";

    public const int NoSelfCollisionsLayer = 11;

    [Obsolete("Use relevant variable in frog")]
    public const float respawnSetBack = 25;

    public const float CameraVeiwRangeApprox = 18;

    //Session Settings
    public enum PlayerMode {single, SplitScreen};
    public static PlayerMode playerMode = single;

    public enum Platform { PC, Android }

#if UNITY_ANDROID
    public const Platform platform = Platform.Android;
#elif UNITY_STANDALONE
    public const Platform platform = Platform.PC;
#endif

    public static void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
