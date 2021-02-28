using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace waveScripts
{
    public class WaveManager : MonoBehaviour
    {
        /*
         * WAVE RULES:
         * 
         * Waves will be released a period of time after a frog leaves the start platform
         * 
         * no additional waves will be released untill that one has left the start platform <-
         * 
         * another can be released after then if a different player leaves the platform
         * 
         * waves will break if there are no frogs ahead of them
         * 
         * 
         * if there is allready a wave behind a frog, dont need another one <-
         */

        [SerializeField] public Level level;
        [SerializeField] GameObject wavesParentComponent;
        [SerializeField] GameObject wavePrefab;
        [SerializeField] public WaveFrogMediatior frogMediatior;

        public List<Wave> waves { get; private set; } = new List<Wave>();

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
                GetInactiveWave().StartWave();
            }

            canRelease = false;
        }

        Wave GetInactiveWave()
        {
            Wave inactiveWave;

            foreach (Wave wave in waves)
            {
                if(wave.state == Wave.State.inactive)
                {
                    inactiveWave = wave;
                    return inactiveWave;
                }
            }

            inactiveWave = Instantiate(wavePrefab).GetComponent<Wave>();
            inactiveWave.Setup(this);
            inactiveWave.transform.parent = wavesParentComponent.transform;
            waves.Add(inactiveWave);
            return inactiveWave;
        }
    }
}
