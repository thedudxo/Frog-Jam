using System.Collections;
using UnityEngine;
using Characters.Instances.Deaths;

namespace Frogs.Instances.Deaths
{
    public class FrogDeadState : IDeathState
    {
        readonly FrogDeathContext context;
        readonly Frog frog;
        readonly FrogComponentsToggle componentsToggle;
        public DeathInformation death;

        float respawnWaitTimer = 0;


        public FrogDeadState(FrogDeathContext context, DeathInformation deathInformation)
        {
            this.context = context;
            frog = context.frog;
            death = deathInformation;
            componentsToggle = context.componentsToggle;
            Die();
        }

        public void UpdateState()
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
            context.ChangeState(new FrogAliveState(context));
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