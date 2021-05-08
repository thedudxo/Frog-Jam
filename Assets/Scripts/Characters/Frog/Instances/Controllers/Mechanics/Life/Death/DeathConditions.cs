using UnityEngine;

namespace Frogs.Instances.Death
{

    public class DeathConditions : MonoBehaviour
    {
        const float deathBellowY = -6.5f;

        [SerializeField] Frog frog;

        bool BelowMinY => frog.transform.position.y < deathBellowY;

        public DeathType Check()
        {
            if (frog.state == FrogState.State.Hidden) return DeathType.none;

            if (BelowMinY)
            {
                Statistics.waterDeaths++;
                return TrySetBack();
            }


            if (Input.GetKeyDown(frog.controllers.input.suicide.key))
            {
                
                Statistics.suicideDeaths++;
                if (frog.state == FrogState.State.Hidden)
                {
                    return DeathType.restart;
                }
                else return TrySetBack();
            }


            bool frogOnStartPlatform = frog.state == FrogState.State.StartPlatform;
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
                if (frog.inDanger)
                    return DeathType.restart;
                else
                    return DeathType.setback;
            }
        }


        DeathType CheckTouching()
        {
            bool isTouchingDeadly = false;
            bool causesRestart = false;

            foreach (GameObject thing in frog.currentCollisions)
            {
                switch (thing.tag)
                {
                    case GM.enemyAligator:
                        Statistics.aligatorDeaths++;
                        isTouchingDeadly = true;
                        break;

                    case Waves.Wave.Tag:
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
