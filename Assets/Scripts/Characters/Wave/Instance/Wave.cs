using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace WaveScripts
{
    public class Wave : MonoBehaviour
    {
        public const string Tag = "Wave";

        [HideInInspector] public WaveCollection collection;
        [HideInInspector] public Level level;

        [Header("Components")]
        [SerializeField] public Controllers controllers;
        [SerializeField] public WaveBreakControlls breakControlls;
        [SerializeField] public WaveSegmentManager segments;
        [SerializeField] public PursuerController pursuer;

        public Vector2 spawnPosition;

        public enum State { normal, breaking, inactive }
        public State state = State.inactive;

        public void Setup(WaveCollection collection)
        {
            this.collection = collection;
            this.level = collection.level;

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

        public float GetXPos()
        {
            return transform.position.x;
        }
    }
}
