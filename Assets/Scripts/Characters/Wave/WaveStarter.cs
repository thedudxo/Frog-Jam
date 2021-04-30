using UnityEngine;

namespace WaveScripts
{
    public class WaveStarter : MonoBehaviour
    {
        [SerializeField] WaveCollection waveManager;

        private Wave StartWave()
        {
            var wave = waveManager.GetInactiveWave();
            wave.StartWave();
            return wave;
        }
    }
}