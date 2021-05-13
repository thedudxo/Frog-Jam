using UnityEngine;
namespace Frogs.Instances.Deaths
{
    class EndLevelRespawnMethod : FrogRespawnMethod
    {
        override public int Priority => -1;
        public EndLevelRespawnMethod(Frog frog, FrogComponentsToggle components) : base(frog, components)
        {
            
        }

        public void PrepareRespawn()
        {
            frog.events.SendBeforeRestart();

            frog.rb.velocity = Vector2.zero;
            frog.transform.position = frog.spawnpoint;
            frog.transform.rotation = Quaternion.identity;

            frog.events.SendRestart();
        }
    }
}
