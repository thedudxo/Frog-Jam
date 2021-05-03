using System.Collections.Generic;
using UnityEngine;
using Levels;
using System.Linq;

namespace Waves
{
    public class WaveCollection : MonoBehaviour
    {

        [Header("External")]
        [SerializeField] public Level level;

        [Header("Components")]
        [SerializeField] GameObject wavesParentComponent;
        [SerializeField] GameObject wavePrefab;
        [SerializeField] public WaveStarter waveStarter;

        public List<Wave> waves { get; private set; } = new List<Wave>();

        public Wave GetInactiveWave()
        {
            bool Inactive(Wave wave) => wave.state == Wave.State.inactive;

            //I'd do the rest of this in one line, but unity doesnt want me to use '??'
            //;~;
            var inactiveWave = waves.FirstOrDefault(Inactive);

            if (inactiveWave == null) return NewWave();
            else return inactiveWave;
        }

        Wave NewWave()
        {
            var wave = Instantiate(wavePrefab).GetComponent<Wave>();
            wave.Setup(this);
            wave.transform.parent = wavesParentComponent.transform;

            waves.Add(wave);
            return wave;
        }

        bool WaveActiveFilter(Wave wave) => wave.state == Wave.State.normal;
        public Wave ClosestBehind(float pos) => FindClosest.Behind(waves,pos,WaveActiveFilter);
        public Wave ClosestAhead(float pos) => FindClosest.Ahead(waves, pos, WaveActiveFilter);
    }
}
