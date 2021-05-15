using Characters.Instances.Deaths;
using UnityEngine;

namespace Frogs.Instances.State
{
    class SetbackRespawnMethod : FrogRespawnMethod
    {
        const int respawnHeight = 5;
        Vector2 Position => frog.transform.position;

        override public int Priority => 0;

        public SetbackRespawnMethod(FrogStateContext context) : base(context){}

        bool IsOnStartPlatform(float position) 
        {
            Levels.Level Level = frog.currentLevel;
            float startPlatformEnd = Level.region.start + Level.StartPlatformLength;
            return position < startPlatformEnd;
        }

        public override void Respawn()
        {
            base.Respawn();

            float setbackPos = Position.x - frog.SetbackDistance;

            if (IsOnStartPlatform(setbackPos))
            {
                new RestartRespawnMethod(context).Respawn();
            }

            else
            {
                frog.transform.position = new Vector2(setbackPos, respawnHeight);
                frog.transform.rotation = Quaternion.identity;

                frog.events.SendOnSetback();

                context.ChangeState(context.alive);
            }
        }
    }
}
