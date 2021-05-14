using UnityEngine;
using Pursuits;

namespace Frogs.Instances
{
    public class DangerState : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Animator dangerAnimation;

        FrogRunner frogPursuit;
        private void Start()
        {
            frogPursuit = frog.FrogRunner;
        }

        private void Update()
        {
            bool danger = false;
            Pursuer behind = frogPursuit.runner?.pursuerBehind;

            if (behind != null)
            {
                float respawnTime = frog.controllers.stateContext.respawnTime;

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
