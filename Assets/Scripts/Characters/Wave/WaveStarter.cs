using UnityEngine;
using Pursuits;

namespace WaveScripts
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