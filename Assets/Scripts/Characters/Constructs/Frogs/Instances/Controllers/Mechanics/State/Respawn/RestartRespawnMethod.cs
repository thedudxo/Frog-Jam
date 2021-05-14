using UnityEngine;
namespace Frogs.Instances.State
{
    class RestartRespawnMethod : FrogRespawnMethod
    {
        Rigidbody2D rb;

        public RestartRespawnMethod(FrogStateContext context) : base(context)
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

            context.ChangeState(new FrogGhostState(context));
        }
    }
}
