using UnityEngine;
using Pursuits;

namespace Frogs
{
    public class DangerState : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Animator dangerAnimation;

        FrogRunner frogPursuit;
        RespawnTimer respawnTimer;
        private void Start()
        {
            frogPursuit = frog.FrogRunner;
            respawnTimer = frog.controllers.life.respawnTimer;
        }

        private void Update()
        {
            bool danger = false;
            Pursuer behind = frogPursuit.runner?.pursuerBehind;

            if (behind != null)
            {
                float respawnTime = respawnTimer.respawnWaitSeconds;

                float pursuerPosAtRespawn = (respawnTime * behind.speed) + behind.position;
                float RunnerposAtRespawn = frogPursuit.runner.position - frog.SetbackDistance;

                danger = pursuerPosAtRespawn >= RunnerposAtRespawn;
            }

            if (frog.state == FrogState.State.StartPlatform) danger = false;

            dangerAnimation.SetBool("Danger", danger);
            frog.inDanger = danger;
        }
    }
}
