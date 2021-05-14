using UnityEngine;

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

        public FrogAliveState alive;
        public FrogDeadState dead;
        public FrogEndLevelState endLevel;

        private void Start()
        {
            alive = new FrogAliveState(this);
            dead = new FrogDeadState(this);
            endLevel = new FrogEndLevelState(this);

            state = alive;
            alive.ghost.Activate();
        }

        public void ChangeState(FrogState newState)
        {
            state = newState;
        }

        private void Update()
        {
            state.UpdateState();
        }
    }
}