using UnityEngine;
using Pursuits;

using WaveScripts;

namespace Characters
{
    public class PursuitHandler : MonoBehaviour
    {
        [SerializeField] WaveCollection waves;
        [SerializeField] FrogCollection frogs;

        Wave IncomingWave = null;
        [HideInInspector] public Pursuit pursuit = new Pursuit();

        public Runner AddRunner()
        {
            if(IncomingWave == null)
            {
                waves.waveStarter.StartWave(pursuit.Add<Pursuer>());
            }

            return pursuit.Add<Runner>();
        }
    }
}