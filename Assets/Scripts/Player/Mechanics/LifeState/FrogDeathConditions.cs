using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDeathConditions
{
    Transform transform;
    const KeyCode suicideKey = KeyCode.Q;
    List<GameObject> currentCollisions;

    const float deathBellowY = -5;

    public enum DeathType { none, setback, restart }


    public FrogDeathConditions(Transform frogTransform, List<GameObject> currentCollisions)
    {
        transform = frogTransform;
        this.currentCollisions = currentCollisions;
    }


    bool BelowMinY => transform.position.y < deathBellowY;

    public DeathType GetDeathType()
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
