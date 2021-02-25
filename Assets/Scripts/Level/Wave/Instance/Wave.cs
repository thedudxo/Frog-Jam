using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace waveScripts
{
    public class Wave : MonoBehaviour
    {

        [SerializeField] public Level level;
        public FrogManager frogManager { get; private set; }

        [SerializeField] WaveRestartConditions waveRestartConditions;
        [SerializeField] WaveSegmentManager waveSegments;

        public float breakPosition;
        public Vector2 spawnPosition;

        public enum State { normal, breaking }
        State state;

        void Awake()
        {
            frogManager = level.frogManager;
            state = State.normal;
            spawnPosition = transform.position;
        }

        private void Update()
        {
            switch (state)
            {
                case State.normal:
                    break;

                case State.breaking:
                    waveSegments.HideAtBreakpoint();
                    break;
            }
        }

        public void StartWave()
        {
            if (state == State.normal) return;
            state = Wave.State.normal;

            transform.position = spawnPosition;
            waveSegments.UnHideSegments();
        }

        public void BreakWave()
        {
            if (state == State.breaking) return;
            state = Wave.State.breaking;

            breakPosition = transform.position.x;
        }

    }
}
