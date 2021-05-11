using UnityEngine;
using Pursuits;

namespace Waves
{
    public class WaveStarter : MonoBehaviour
    {
        [SerializeField] WaveCollection waveManager;

        public Wave StartWave(Pursuer pursuer)
        {
            var wave = waveManager.GetInactiveWave();
            wave.StartWave(pursuer);
            return wave;
        }
    }
}