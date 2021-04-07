using UnityEngine;
using Chaseables;

namespace WaveScripts
{
    public class WaveStarter : MonoBehaviour
    {
        [SerializeField] WaveCollection waveManager;

        public IChaser Chase(IChaseable chaseable)
        {

            Wave waveBehind = waveManager.ClosestBehind(chaseable.GetXPos());

            if (waveBehind == null)
                return StartWave();

            else return waveBehind;
        }

        private Wave StartWave()
        {
            var wave = waveManager.GetInactiveWave();
            wave.StartWave();
            return wave;
        }
    }
}