using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts.Life
{
    public class DeathConditions
    {
        Transform transform;
        const KeyCode suicideKey = KeyCode.Q;
        List<GameObject> currentCollisions;

        const float deathBellowY = -6.5f;

        public enum DeathType { none, setback, restart }


        public DeathConditions(Transform frogTransform, List<GameObject> currentCollisions)
        {
            transform = frogTransform;
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

            DeathType touchingDeathType = CheckTouching();
            if (touchingDeathType != DeathType.none)
            {
                return touchingDeathType;
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
