using UnityEngine;
using System.Linq;
using static Waves.Wave.State;

namespace Waves
{
    public class WaveBreakControlls : MonoBehaviour
    {
        [SerializeField] Wave wave;

        public float BreakPosition { get; private set; }
        Transform waveTransform;
        bool ReachedEndOfLevel => waveTransform.position.x > wave.level.region.end;

        private void Start()
        {
            waveTransform = wave.transform;
        }

        private void Update()
        {
            if (wave.state == Wave.State.normal)
            {
                if (ReachedEndOfLevel)
                {
                    BreakWave();
                }
            }
        }

        public void BreakWave()
        {
            if (wave.state != normal)
            {
                Debug.LogWarning($"Tried breaking wave in state '{wave.state}', expected normal", this);
                return;
            }

            BreakPosition = transform.position.x;
            wave.state = breaking;
            Debug.Log("breaking");
        }

        public void StopBreaking()
        {
            if (wave.state != breaking)
            {
                Debug.LogWarning($"Tried to stop breaking on a wave whos state was '{wave.state}', expected breaking", this);
                return;
            }
            wave.state = inactive;
            wave.pursuerController.RemovePursuer();
        }

    }
}