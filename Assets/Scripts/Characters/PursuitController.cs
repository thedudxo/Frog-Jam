﻿using UnityEngine;
using Pursuits;
using Waves;
using Frogs.Collections;

namespace Characters
{
    public class PursuitController : MonoBehaviour
    {
        [SerializeField] WaveCollection waves;
        [SerializeField] FrogCollection frogs;

        Wave IncomingWave = null;
        [HideInInspector] public Pursuit pursuit = new Pursuit();

        readonly float tickspeed = 1f;
        float timeSinceLastTick = 0;


        public Runner AddRunner()
        {
            if(IncomingWave == null)
            {
                waves.waveStarter.StartWave(pursuit.Add<Pursuer>());
            }

            return pursuit.Add<Runner>();
        }

        public void Tick()
        {
            pursuit.Tick();
            timeSinceLastTick = 0;
        }

        private void Update()
        {
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= tickspeed)
            {
                Tick();
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