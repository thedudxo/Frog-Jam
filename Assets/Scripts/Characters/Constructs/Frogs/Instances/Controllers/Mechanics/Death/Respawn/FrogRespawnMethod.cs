using Characters.Instances.Deaths;
namespace Frogs.Instances.Deaths
{
    abstract class FrogRespawnMethod : IRespawnMethod
    {
        protected Frog frog;
        FrogComponentsToggle components;
        public FrogRespawnMethod(Frog frog, FrogComponentsToggle components)
        {
            this.frog = frog;
            this.components = components;
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
