using Frogs.Instances.State;

namespace Frogs.Instances
{
    public class FrogStateInfo
    {
        Frog frog;
        FrogAliveState aliveState;
        FrogStateContext context;
        public FrogStateInfo(FrogStateContext context)
        {
            this.context = context;
            frog = context.frog;
            aliveState = frog.controllers.stateContext.alive;
        }

        public bool OnStartPlatform => aliveState.startPlatform.OnStartPlatform;
        public bool Ghosted => aliveState.ghost.IsActive;

        public bool inDanger = false;
    }
}