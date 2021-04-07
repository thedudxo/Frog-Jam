using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaveScripts;
using Chaseables;

namespace FrogScripts {
    public class DangerState : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Animator dangerAnimation;

        FrogChaseable chaseable;

        private void Start()
        {
            chaseable = frog.chaseable;
        }

        private void Update()
        {
            bool danger = chaseable.WillSetbackBehindAChaser(
                frog.SetbackDistance, 
                frog.lifeController.respawnTimer.respawnWaitSeconds
                );

            if (frog.state == FrogState.State.StartPlatform) danger = false;

            dangerAnimation.SetBool("Danger", danger);
            frog.inDanger = danger;
        }
    }
}
