using System.Collections;
using UnityEngine;
using Pursuits;
using System.Linq;

using WaveScripts;
using FrogScripts;

namespace Characters
{
    public class PursuitHandler : MonoBehaviour
    {
        [SerializeField] WaveCollection waves;
        [SerializeField] FrogCollection frogs;

        Wave IncomingWave = null;
        [HideInInspector] public Pursuit pursuit;

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