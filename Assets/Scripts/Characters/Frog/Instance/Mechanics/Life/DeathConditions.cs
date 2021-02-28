using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts.Life
{
    public class DeathConditions
    {
        Transform transform;
        KeyCode suicideKey;
        List<GameObject> currentCollisions;

        const float deathBellowY = -6.5f;

        public enum DeathType { none, setback, restart }
        Frog frog;
        WaveFrogMediatior waveMediator;

        public DeathConditions(Frog frog, List<GameObject> currentCollisions)
        {
            this.frog = frog;
            transform = frog.transform;
            suicideKey = frog.controlls.suicideKey;
            this.currentCollisions = currentCollisions;
            waveMediator = frog.currentLevel.waveFrogMediatior;
        }


        bool BelowMinY => transform.position.y < deathBellowY;

        public DeathType Check()
        {
            if (BelowMinY)
            {
                Statistics.waterDeaths++;
                return TrySetBack();
            }

            if (Input.GetKeyDown(suicideKey))
            {
                Statistics.suicideDeaths++;
                return TrySetBack(); 
            }

            bool frogOnStartPlatform = frog.location == FrogLocationTracker.Location.StartPlatform;
            if (frogOnStartPlatform == false)
            {

                DeathType touchingDeathType = CheckTouching();
                if (touchingDeathType != DeathType.none)
                {
                    return touchingDeathType;
                }

            }
            return DeathType.none;

            DeathType TrySetBack()
            {
                if (waveMediator.FrogWillSetbackBehindWave(frog)) return DeathType.restart;
                return DeathType.setback;
            }
        }


        DeathType CheckTouching()
        {
            bool isTouchingDeadly = false;
            bool causesRestart = false;

            foreach (GameObject thing in currentCollisions)
            {
                switch (thing.tag)
                {
                    case GM.enemyAligator:
                        Statistics.aligatorDeaths++;
                        isTouchingDeadly = true;
                        break;

                    case waveScripts.Wave.Tag:
                        Statistics.waveDeaths++;
                        isTouchingDeadly = true;
                        causesRestart = true;
                        break;
                }
            }

            if (!isTouchingDeadly) return DeathType.none;
            if (causesRestart) return DeathType.restart;
            else return DeathType.setback;
        }
    }
}
