using System.Collections.Generic;
using UnityEngine;
using Characters.Instances.Deaths;

namespace Frogs.Instances.Deaths
{

    public class FrogAliveState : IDeathState
    {
        FrogDeathContext context;
        Frog frog;
        DeathConditions deathConditions;
        SetbackRespawnMethod setback;
        RestartRespawnMethod restart;

        public FrogAliveState(FrogDeathContext context)
        {
            this.context = context;
            frog = context.frog;

            restart = new RestartRespawnMethod(frog, context.componentsToggle);
            setback = new SetbackRespawnMethod(frog, restart, context.componentsToggle);

            restart.Respawn();

            deathConditions = new DeathConditions
            (
                new List<IDeathCondition>()
                {
                    new BelowYDeathCondition(frog.gameObject, -6.5f, setback),
                    new TouchDeadlyDeathCondition(frog.currentCollisions, restart),
                    new PressKeyDeathCondition(frog.controllers.input.suicide.key, setback)
                }
            );
        }

        bool PlayerGotToTheEnd => frog.transform.position.x >= frog.currentLevel.region.end;
        public void UpdateState()
        {
            if (PlayerGotToTheEnd)
            {
                context.ChangeState(new FrogEndLevelState(context));
                return;
            }

            DeathInformation death;
            death = deathConditions.Check();
            if(death != null)
            {
                context.ChangeState(new FrogDeadState(context, death));
                return;
            }
        }
    }
}