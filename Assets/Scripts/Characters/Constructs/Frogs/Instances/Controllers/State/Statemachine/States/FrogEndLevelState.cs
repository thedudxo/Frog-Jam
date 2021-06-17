using UnityEngine;

namespace Frogs.Instances.State
{
    public class FrogEndLevelState : FrogState
    {
        bool PlayerInputRestart => Input.GetKeyDown(frog.controllers.input.GetKeybind(Inputs.Action.Suicide));

        readonly EndLevelRespawnMethod respawnMethod;

        public FrogEndLevelState(FrogStateContext context) : base(context)
        {
            respawnMethod = new EndLevelRespawnMethod(context);
        }

        public void Activate()
        {
            frog.events.SendOnEndLevel();
            context.levelStats.CheckForPBTime();

            context.levelEndScreen.Enable(
                context.frogTime.CurrentLevelTime,
                (float)context.levelStats.PbTime,
                context.splitFX.TotalSplitTime);

            respawnMethod.PrepareRespawn();
        }

        public void ExitState()
        {
            context.levelEndScreen.Disable();
            respawnMethod.Respawn();
        }

        public override void UpdateState()
        {
            if (PlayerInputRestart)
            {
                ExitState();
            }
        }
    }
}
