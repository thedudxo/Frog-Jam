using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogRespawn {

    Vector2 levelStart;
    const float respawnSetBack = 25;
    const int respawnHeight = 5;

    Transform transform;

    Wave wave;


    public FrogRespawn(Vector2 levelStart, Transform transform, Wave wave)
    {
        this.levelStart = levelStart;
        this.transform = transform;
        this.wave = wave;
    }

    bool FrogIsOnStartPlatform => transform.position.x < (levelStart.x + GM.currentLevel.spawnPlatformLength);

    public void Setback()
    {
        Vector2 respawnPosition = new Vector2(transform.position.x - respawnSetBack, respawnHeight);
        transform.position = respawnPosition;

        if (FrogIsOnStartPlatform)
        {
            Restart();
            return;
        }

        wave.Setback(respawnSetBack);
    }

    public void Restart()
    {
        transform.position = levelStart;
        wave.Restart();
        GM.splitManager.currentTime = 0;
        GM.LevelRestart();
    }
}
