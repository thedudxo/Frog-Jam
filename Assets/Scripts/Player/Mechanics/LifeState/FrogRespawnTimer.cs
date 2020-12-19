using System;
using UnityEngine;


public class FrogRespawnTimer
{
    const float respawnWaitSeconds = 1;
    float respawnWaitTimer = 0;

    public bool ShouldRespawnNow()
    {
        respawnWaitTimer += Time.deltaTime;

        if (GM.gameMusic.IsBeatFrame && respawnWaitTimer >= respawnWaitSeconds)
        {
            respawnWaitTimer = 0;
            return true;
        }

        return false;
    }
}
