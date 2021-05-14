using System.Collections;
using UnityEngine;
using Characters.Instances.Deaths;


namespace Frogs.Instances.State
{
    public class FrogStateContext : MonoBehaviour
    {
        [SerializeField] public Frog frog;
        [SerializeField] public FrogComponentsToggle componentsToggle;

        [SerializeField] public LevelEndScreen levelEndScreen;
        [SerializeField] public LevelStats levelStats;
        [SerializeField] public SplitEffectsManager splitFX;
        [SerializeField] public FrogTime frogTime;

        [HideInInspector] public float respawnTime = 1f;

        public FrogState state { get; private set; }

        private void Start()
        {
            state = new FrogGhostState(this);
        }

        public void ChangeState(FrogState newState)
        {
            state.ExitState();
            state = newState;
        }

        private void Update()
        {
            state.UpdateState();
        }
    }
}