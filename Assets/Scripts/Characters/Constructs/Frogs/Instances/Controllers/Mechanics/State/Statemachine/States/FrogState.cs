namespace Frogs.Instances.State
{
    public abstract class FrogState
    {
        protected FrogStateContext context;
        protected Frog frog;

        public FrogState(FrogStateContext context)
        {
            if (context == null) throw new System.ArgumentNullException("context");
            this.context = context;
            frog = context.frog;
        }

        public abstract void UpdateState();
        public abstract void ExitState();
    }
}