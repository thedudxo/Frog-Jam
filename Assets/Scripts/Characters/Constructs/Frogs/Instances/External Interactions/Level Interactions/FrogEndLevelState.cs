using UnityEngine;

namespace Frogs.Instances.Deaths
{
    public class FrogEndLevelState : IDeathState
    {
        FrogDeathContext context;
        Frog frog;

        bool PlayerInputRestart => Input.GetKeyDown(frog.controllers.input.suicide.key);

        public FrogEndLevelState(FrogDeathContext context)
        {
            this.context = context;
            frog = context.frog;


            frog.events.SendOnEndLevel();
            context.levelStats.CheckForPBTime();

            context.levelEndScreen.Enable(
                context.frogTime.CurrentLevelTime,
                (float)context.levelStats.PbTime,
                context.splitFX.TotalSplitTime);
        }
      
        public void UpdateState()
        {
            if (PlayerInputRestart)
            {
                context.levelEndScreen.Disable();
                new RestartRespawnMethod(frog, context.componentsToggle).Respawn();
                context.ChangeState(new FrogAliveState(context));
            }
        }
    }
}
