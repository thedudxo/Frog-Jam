using UnityEngine;
using Characters.Instances.Deaths;

namespace Frogs.Instances.State
{
    public class FrogDeadState : FrogState
    {
        readonly FrogComponentsToggle componentsToggle;
        public DeathInformation death;

        float respawnWaitTimer = 0;


        public FrogDeadState(FrogStateContext context) : base(context)
        {
            componentsToggle = context.componentsToggle;
        }

        public override void UpdateState()
        {
            respawnWaitTimer += Time.deltaTime;

            if (respawnWaitTimer >= context.respawnTime)
            {
                respawnWaitTimer = 0;
                Respawn();
            }
        }

        void Respawn()
        {
            death.respawnMethod.Respawn();
        }

        public void Activate(DeathInformation death)
        {
            this.death = death;

            frog.events.SendBeforeDeath();


            Statistics.totalDeaths++;
            frog.controllers.audio.deathSound.PlayRandom();
            componentsToggle.ToggleComponents(false);

            frog.events.SendOnDeath();
        }
    }
}