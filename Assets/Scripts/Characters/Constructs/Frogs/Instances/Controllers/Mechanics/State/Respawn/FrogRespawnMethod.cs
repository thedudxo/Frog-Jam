using Characters.Instances.Deaths;
namespace Frogs.Instances.State
{
    abstract class FrogRespawnMethod : IRespawnMethod
    {
        protected Frog frog;
        protected FrogStateContext context;
        protected FrogComponentsToggle components;
        public FrogRespawnMethod(FrogStateContext context)
        {
            if (context == null) throw new System.ArgumentNullException("context");

            this.context = context;
            this.frog = context.frog;
            this.components = context.componentsToggle;
        }

        public abstract int Priority { get; }
        public virtual void Respawn()
        {
            frog.controllers.audio.respawnSound.PlayRandom();
            components.ToggleComponents(true);
            frog.events.SendOnAnyRespawn();
        }
    }
}
