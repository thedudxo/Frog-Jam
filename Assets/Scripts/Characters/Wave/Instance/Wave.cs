using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;
using Chaseables;

namespace WaveScripts
{
    public class Wave : MonoBehaviour, IChaser
    {
        public const string Tag = "Wave";

        [HideInInspector] public WaveCollection collection;
        [HideInInspector] public Level level;
        [HideInInspector] public FrogManager frogManager { get; private set; }

        [Header("Components")]
        [SerializeField] public WaveBreakControlls breakControlls;
        [SerializeField] public WaveSegmentManager segments;

        public Vector2 spawnPosition;

        public enum State { normal, breaking, inactive }
        public State state = State.inactive;

        public void Setup(WaveCollection collection)
        {
            this.collection = collection;
            this.level = collection.level;
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

        public bool IsBehind(IChaseable chaseable)
        {
            return chaseable.GetXPos() < GetXPos();
        }

        public void CheckStopChaseConditions()
        {
            breakControlls.CheckIfWaveShouldBreak();
        }

        public float GetXPos()
        {
            return transform.position.x;
        }

        public float GetSpeed()
        {
            return WaveMovement.speed;
        }
    }
}
