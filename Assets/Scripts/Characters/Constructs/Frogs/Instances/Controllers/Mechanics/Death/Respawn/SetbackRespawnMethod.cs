using Characters.Instances.Deaths;
using UnityEngine;

namespace Frogs.Instances.Deaths
{
    class SetbackRespawnMethod : FrogRespawnMethod
    {
        const int respawnHeight = 5;
        readonly RestartRespawnMethod restartMethod;
        Vector2 Position => frog.transform.position;

        override public int Priority => 1;

        public SetbackRespawnMethod(Frog frog, RestartRespawnMethod restartRespawnMethod, FrogComponentsToggle componentsToggle) : base(frog, componentsToggle)
        {
            this.restartMethod = restartRespawnMethod;
        }

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
                restartMethod.Respawn();
            }

            else
            {
                frog.transform.position = new Vector2(setbackPos, respawnHeight);
                frog.transform.rotation = Quaternion.identity;

                frog.events.SendOnSetback();
            }
        }
    }
}
