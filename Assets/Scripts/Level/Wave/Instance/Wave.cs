using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace WaveScripts
{
    public class Wave : MonoBehaviour
    {
        public const string Tag = "Wave";

        [HideInInspector] public WaveManager manager;
        [HideInInspector] public Level level;
        [HideInInspector] public FrogManager frogManager { get; private set; }

        [Header("Components")]
        [SerializeField] public WaveBreakControlls breakControlls;
        [SerializeField] public WaveSegmentManager segments;

        public float breakPosition;
        public Vector2 spawnPosition;

        public enum State { normal, breaking, inactive }
        public State state;

        public void Setup(WaveManager manager)
        {
            this.manager = manager;
            this.level = manager.level;
            this.frogManager = level.frogManager;

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
                    segments.HideAtBreakpoint();
                    break;
            }
        }

        public void StartWave()
        {
            if (state != State.inactive)
            {
                Debug.LogWarning("Tried starting a wave that wasn't inactive",this);
                return;
            }

            segments.UnHideSegments();
            transform.position = spawnPosition;
            state = Wave.State.normal;
        }

        public void BreakWave()
        {
            if (state != State.normal)
            {
                Debug.LogWarning("Tried breaking a wave which wasn't in normal state", this);
                return;
            }

            breakPosition = transform.position.x;
            state = Wave.State.breaking;
        }

        public void FinishedBreaking()
        {
            if (state != State.breaking)
            {
                Debug.LogWarning("Tried setting non breaking wave as inactive", this);
                return;
            }
                state = State.inactive;
        }

    }
}
