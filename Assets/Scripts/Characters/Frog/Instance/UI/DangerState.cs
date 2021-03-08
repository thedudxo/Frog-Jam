using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts {
    public class DangerState : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] Animator dangerAnimation;

        private void Update()
        {
            bool danger = frog.currentLevel.waveFrogMediatior.FrogWillSetbackBehindWave(frog);
            if (frog.state == FrogState.State.StartPlatform) danger = false;
            dangerAnimation.SetBool("Danger", danger);
            frog.inDanger = danger;
        }
    }
}
