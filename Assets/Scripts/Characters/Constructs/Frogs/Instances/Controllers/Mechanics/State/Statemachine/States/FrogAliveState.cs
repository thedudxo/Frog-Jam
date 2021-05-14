using System.Collections.Generic;
using UnityEngine;
using Characters.Instances.Deaths;

namespace Frogs.Instances.State
{

    public class FrogAliveState : FrogState
    {
        DeathConditions deathConditions;
        SetbackRespawnMethod setback;
        RestartRespawnMethod restart;

        public FrogAliveState(FrogStateContext context) : base(context)
        {

            restart = new RestartRespawnMethod(context);
            setback = new SetbackRespawnMethod(restart, context);

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
        public override void UpdateState()
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

        public override void ExitState() { }
    }
}