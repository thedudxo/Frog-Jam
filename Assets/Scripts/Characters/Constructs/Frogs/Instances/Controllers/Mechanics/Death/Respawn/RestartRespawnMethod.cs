using UnityEngine;
namespace Frogs.Instances.Deaths
{
    class RestartRespawnMethod : FrogRespawnMethod
    {
        Rigidbody2D rb;

        public RestartRespawnMethod(Frog frog, FrogComponentsToggle componentsToggle) 
            : base(frog, componentsToggle)
        {
            rb = frog.rb;
        }

        override public int Priority => 1;

        override public void Respawn()
        {
            base.Respawn();

            frog.events.SendBeforeRestart();

            rb.velocity = Vector2.zero;
            frog.transform.position = frog.spawnpoint;
            frog.transform.rotation = Quaternion.identity;

            frog.events.SendRestart();
        }
    }
}
