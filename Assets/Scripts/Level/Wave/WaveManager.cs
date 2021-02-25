using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace waveScripts
{
    public class WaveManager : MonoBehaviour
    {
        /*
         * WAVE RULES:
         * 
         * Waves will be released a period of time after a frog leaves the start platform
         * 
         * no additional waves will be released untill that one has left the start platform
         * 
         * another can be released after then if a different player leaves the platform
         * 
         * waves will break if there are no frogs ahead of them
         */

        List<Wave> waves = new List<Wave>();

        float waveReleaseCooldown = 2f;
        float releaseTimer = 0;
        bool canRelease = true;

        private void Update()
        {
            if (canRelease == false)
            {
                releaseTimer += Time.deltaTime;

                if (releaseTimer > waveReleaseCooldown)
                {
                    releaseTimer = 0;
                    canRelease = true;
                }
            }
        }

        public void ReleaseWave()
        {
            if (canRelease)
            {
                //wave.whatevs
            }

            canRelease = false;
        }
    }
}
