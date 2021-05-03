using UnityEngine;
using Pursuits;
using Waves;
using Frogs.Collections;

namespace Characters
{
    public class PursuitHandler : MonoBehaviour
    {
        [SerializeField] WaveCollection waves;
        [SerializeField] FrogCollection frogs;

        Wave IncomingWave = null;
        [HideInInspector] public Pursuit pursuit = new Pursuit();

        readonly float tickspeed = 0.5f;
        float timeSinceLastTick = 0;

        public Runner AddRunner()
        {
            if(IncomingWave == null)
            {
                waves.waveStarter.StartWave(pursuit.Add<Pursuer>());
            }

            return pursuit.Add<Runner>();
        }

        private void Update()
        {
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= tickspeed)
            {
                pursuit.Tick();
                timeSinceLastTick = 0;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                foreach (string s in pursuit.LastTickLog)
                {
                    print(s);
                }
            }
        }
    }
}