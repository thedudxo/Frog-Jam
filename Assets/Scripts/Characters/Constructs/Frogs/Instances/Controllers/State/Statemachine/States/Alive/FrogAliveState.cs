﻿using UnityEngine;
using Characters.Instances.Deaths;

namespace Frogs.Instances.State
{
    public class FrogAliveState : FrogState
    {
        FrogDeathConditions conditions;

        public FrogGhost ghost;
        public StartPlatform startPlatform;

        public FrogAliveState(FrogStateContext context) : base(context)
        {
            conditions = new FrogDeathConditions(context,this);
            ghost = new FrogGhost(frog);
            startPlatform = new StartPlatform(frog, conditions);
        }

        bool PlayerGotToTheEnd => frog.transform.position.x >= frog.currentLevel.region.end;

        public override void UpdateState()
        {

            if (PlayerGotToTheEnd)
            {
                context.ChangeState(context.endLevel);
                context.endLevel.Activate();
                return;
            }

            DeathInformation death;
            death = conditions.Check();
            if(death != null)
            {
                context.ChangeState(context.dead);
                context.dead.Activate(death);
                return;
            }

            ghost.Update();
            startPlatform.Update();
        }
    }
}