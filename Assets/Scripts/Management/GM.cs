using UnityEngine.SceneManagement;
using static GM.PlayerMode;

public static class GM {

    public static LevelScripts.Level currentLevel;
    public static AudioManager audioManager;
    public static GameMusic gameMusic;

    public static bool sendAnyalitics = false;

    //constants
    public const string playerTag = "Phill";
    public const string enemyAligator = "EnemyAligator";

    public const int NoSelfCollisionsLayer = 11;

    public const float respawnSetBack = 25;

    public const float CameraVeiwRangeApprox = 18;

    //Session Settings
    public enum PlayerMode {single, SplitScreen2};
    public static PlayerMode playerMode = single;

    public static void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
