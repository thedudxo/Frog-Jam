using UnityEngine;
using Pursuits;

namespace FrogScripts
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
            float respawnTime = respawnTimer.respawnWaitSeconds;
            Pursuer behind = frogPursuit.runner.pursuerBehind;

            float pursuerPosAtRespawn = (respawnTime * behind.speed) + behind.position;
            float RunnerposAtRespawn = frogPursuit.runner.position - frog.SetbackDistance;

            bool danger = pursuerPosAtRespawn >= RunnerposAtRespawn;

            if (frog.state == FrogState.State.StartPlatform) danger = false;

            dangerAnimation.SetBool("Danger", danger);
            frog.inDanger = danger;
        }
    }
}
