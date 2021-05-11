using UnityEngine;
namespace Frogs.Instances.Deaths
{
    class RestartRespawnMethod : FrogRespawnMethod
    {
        Rigidbody2D rb;
        Vector2 spawnpoint;

        public RestartRespawnMethod(Frog frog, FrogComponentsToggle componentsToggle) 
            : base(frog, componentsToggle)
        {
            rb = frog.rb;
            spawnpoint = new Vector2 (frog.currentLevel.region.start, frog.transform.position.y);
        }

        override public int Priority => 1;

        override public void Respawn()
        {
            base.Respawn();

            frog.events.SendBeforeRestart();

            rb.velocity = Vector2.zero;
            frog.transform.position = spawnpoint;
            frog.transform.rotation = Quaternion.identity;

            frog.events.SendRestart();
        }
    }
}
