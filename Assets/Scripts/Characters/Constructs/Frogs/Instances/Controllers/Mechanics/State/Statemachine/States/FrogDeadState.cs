using System.Collections;
using UnityEngine;
using Characters.Instances.Deaths;

namespace Frogs.Instances.State
{
    public class FrogDeadState : FrogState
    {
        readonly FrogComponentsToggle componentsToggle;
        public DeathInformation death;

        float respawnWaitTimer = 0;


        public FrogDeadState(FrogStateContext context, DeathInformation deathInformation) : base(context)
        {
            death = deathInformation;
            componentsToggle = context.componentsToggle;
            Die();
        }

        public override void ExitState() { }

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

        void Die()
        {
            frog.events.SendBeforeDeath();


            Statistics.totalDeaths++;
            frog.controllers.audio.respawnSound.PlayRandom();
            componentsToggle.ToggleComponents(false);

            frog.events.SendOnDeath();
        }
    }
}