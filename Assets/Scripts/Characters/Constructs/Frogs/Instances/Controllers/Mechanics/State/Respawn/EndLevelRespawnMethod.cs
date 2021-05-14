using UnityEngine;
namespace Frogs.Instances.State
{
    class EndLevelRespawnMethod : FrogRespawnMethod
    {
        override public int Priority => -1;
        public EndLevelRespawnMethod(FrogStateContext context) : base(context)
        {
            
        }

        public void PrepareRespawn()
        {
            frog.events.SendBeforeRestart();

            frog.rb.velocity = Vector2.zero;
            frog.transform.position = frog.spawnpoint;
            frog.transform.rotation = Quaternion.identity;

            frog.events.SendRestart();

            context.ChangeState(new FrogGhostState(context));
        }
    }
}
