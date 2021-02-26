using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace waveScripts
{
    public class Wave : MonoBehaviour
    {
        public WaveManager manager;
        public Level level;
        public FrogManager frogManager { get; private set; }

        [SerializeField] WaveRestartConditions waveRestartConditions;
        [SerializeField] WaveSegmentManager waveSegments;

        public float breakPosition;
        public Vector2 spawnPosition;

        public enum State { normal, breaking, inactive }
        public State state;

        public void Setup(WaveManager manager)
        {
            this.manager = manager;
            level = manager.level;
            frogManager = level.frogManager;
            state = State.inactive;
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
