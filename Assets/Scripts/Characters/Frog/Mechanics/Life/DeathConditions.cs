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

        public DeathConditions(Frog frog, List<GameObject> currentCollisions)
        {
            this.frog = frog;
            transform = frog.transform;
            suicideKey = frog.controlls.suicideKey;
            this.currentCollisions = currentCollisions;
        }


        bool BelowMinY => transform.position.y < deathBellowY;

        public DeathType Check()
        {
            if (BelowMinY)
            {
                Statistics.waterDeaths++;
                return DeathType.setback;
            }

            if (Input.GetKeyDown(suicideKey))
            {
                Statistics.suicideDeaths++;
                return DeathType.setback;
            }

            if (!frog.OnStartingPlatform)
            {

                DeathType touchingDeathType = CheckTouching();
                if (touchingDeathType != DeathType.none)
                {
                    return touchingDeathType;
                }

            }
            return DeathType.none;
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

                    case "Wave":
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
