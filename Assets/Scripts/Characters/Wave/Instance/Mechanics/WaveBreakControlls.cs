using System.Collections;
using UnityEngine;
using FrogScripts;

namespace WaveScripts
{
    public class WaveBreakControlls : MonoBehaviour
    {
        [SerializeField] Wave wave;

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
                    wave.BreakWave();
                }
            }
        }

        public void FrogTriggerBreak()
        {
            bool FrogAliveFilter(Frog frog)
            {
                if (frog.state == FrogState.State.Level) return true;
                return false;
                // without this filter, next frog should never be null since it takes time to die, before moving its position.
            }

            float wavePos = wave.transform.position.x;

            Frog nextFrog = FindClosest.Ahead(wave.frogManager.Frogs, wavePos, FrogAliveFilter);

            if (nextFrog == null || WaveBeforeNextFrog()) 
                wave.BreakWave(); 

            bool WaveBeforeNextFrog()
            {
                Wave nextWave = wave.manager.ClosestWaveAheadPosition(wavePos);
                if (nextWave != null)
                {
                    float nextWavePos = nextWave.transform.position.x;
                    float nextFrogPos = nextFrog.transform.position.x;
                    if (nextWavePos < nextFrogPos)
                        return true;
                }
                return false;
            }
        }
    }
}