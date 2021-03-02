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

        [Header("External")]
        [SerializeField] public Level level;
        [SerializeField] public WaveFrogMediatior frogMediatior;

        [Header("Components")]
        [SerializeField] GameObject wavesParentComponent;
        [SerializeField] GameObject wavePrefab;
        public WaveStarter waveStarter;

        public List<Wave> waves { get; private set; } = new List<Wave>();

        private void Start()
        {
            waveStarter = new WaveStarter(this, frogMediatior);
        }

        private void Update()
        {
            waveStarter.CheckStartConditions();
        }

        public Wave GetInactiveWave()
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
