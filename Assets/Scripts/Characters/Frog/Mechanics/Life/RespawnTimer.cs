using System;
using UnityEngine;

namespace FrogScripts
{
    public class RespawnTimer
    {
        const float respawnWaitSeconds = 1;
        float respawnWaitTimer = 0;

        public bool ShouldRespawnNow()
        {
            respawnWaitTimer += Time.deltaTime;
            bool isBeat = true; // GM.gameMusic.IsBeatFrame;

            if (isBeat && respawnWaitTimer >= respawnWaitSeconds)
            {
                respawnWaitTimer = 0;
                return true;
            }

            return false;
        }
    }
}
