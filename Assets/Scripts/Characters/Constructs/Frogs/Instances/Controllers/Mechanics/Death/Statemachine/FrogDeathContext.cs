using System.Collections;
using UnityEngine;
using Characters.Instances.Deaths;


namespace Frogs.Instances.Deaths
{
    public class FrogDeathContext : MonoBehaviour
    {
        [SerializeField] public Frog frog;
        [SerializeField] public FrogComponentsToggle componentsToggle;

        [SerializeField] public LevelEndScreen levelEndScreen;
        [SerializeField] public LevelStats levelStats;
        [SerializeField] public SplitEffectsManager splitFX;
        [SerializeField] public FrogTime frogTime;

        [HideInInspector] public float respawnTime = 1f;

        public IDeathState state;

        private void Start()
        {
            state = new FrogAliveState(this);
        }

        public void ChangeState(IDeathState newState)
        {
            state = newState;
        }

        private void Update()
        {
            state.UpdateState();
        }
    }
}