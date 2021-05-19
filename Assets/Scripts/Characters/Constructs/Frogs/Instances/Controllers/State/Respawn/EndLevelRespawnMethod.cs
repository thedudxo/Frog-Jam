using UnityEngine;
namespace Frogs.Instances.State
{
    class EndLevelRespawnMethod : FrogRespawnMethod
    {
        override public int Priority => -1;
        public EndLevelRespawnMethod(FrogStateContext context) : base(context)
        {
            
        }

        public override void Respawn()
        {
            base.Respawn();

            frog.controllers.vfx.ShowFrogVisuals(true);
            frog.collider.enabled = true;
            frog.rb.isKinematic = false;
            frog.rb.velocity = Vector2.zero;
            frog.transform.position = frog.spawnpoint;
            frog.transform.rotation = Quaternion.identity;

            context.ChangeState(context.alive);
            context.alive.ghost.Activate();
        }

        public void PrepareRespawn()
        {
            frog.events.SendBeforeRestart();

            frog.rb.velocity = Vector2.zero;
            frog.transform.position = frog.spawnpoint;

            frog.controllers.vfx.ShowFrogVisuals(false);
            frog.collider.enabled = false;
            frog.rb.isKinematic = true;

            frog.events.SendRestart();
        }
    }
}
