using System.Collections.Generic;
using Characters.Instances.Deaths;

namespace Frogs.Instances.State
{
    public class FrogDeathConditions
    {
        DeathConditions deathConditions;

        SetbackRespawnMethod setback;
        RestartRespawnMethod restart;
        SuicideRespawnMethod suicide;

        public BelowYDeathCondition belowY;
        public TouchDeadlyDeathCondition touchDeadly;
        public PressKeyDeathCondition suicideKey;

        Frog frog;
        FrogStateContext context;

        public FrogDeathConditions(FrogStateContext context)
        {
            this.context = context;
            frog = context.frog;

            restart = new RestartRespawnMethod(context);
            setback = new SetbackRespawnMethod(context);
            suicide = new SuicideRespawnMethod(context);

            belowY = new BelowYDeathCondition(frog.gameObject, -6.5f, setback);
            touchDeadly = new TouchDeadlyDeathCondition(frog.currentCollisions, restart);
            suicideKey = new PressKeyDeathCondition(frog.controllers.input.suicide, suicide);

            deathConditions = new DeathConditions
            (
                new List<IDeathCondition>()
                {
                    belowY,
                    touchDeadly,
                    suicideKey
                }
            );
        }

        public DeathInformation Check()
        {
            DeathInformation info = deathConditions.Check();

            if (info != null && frog.state.inDanger  ) 
            {
                info.respawnMethod = new RestartRespawnMethod(context);
            }

            return info;
        }
    }
}