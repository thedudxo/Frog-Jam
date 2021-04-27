using System.Collections.Generic;
using UnityEngine;
using LevelScripts;
using Chaseables.MonoBehaviours;
using Chaseables;
using System.Linq;

namespace WaveScripts
{
    public class WaveCollection : ChaserCollection, IChaserCollection
    {

        [Header("External")]
        [SerializeField] public Level level;

        [Header("Components")]
        [SerializeField] GameObject wavesParentComponent;
        [SerializeField] GameObject wavePrefab;
        [SerializeField] public ChaseableCollection chaseables;
        [SerializeField] WaveStarter waveStarter;

        public List<Wave> waves { get; private set; } = new List<Wave>();
        public override IChaseableCollection Chasing { get; set ; }

        private void Awake()
        {
            Chasing = (IChaseableCollection)level.frogManager;
        }

        public Wave GetInactiveWave()
        {
            bool Inactive(Wave wave) => wave.state == Wave.State.inactive;

            //I'd do the rest of this in one line, but unity doesnt want me to use '??'
            //;~;
            var inactiveWave = waves.FirstOrDefault(Inactive);

            if (inactiveWave == null) return NewWave();
            else return inactiveWave;
        }

        Wave NewWave()
        {
            var wave = Instantiate(wavePrefab).GetComponent<Wave>();
            wave.Setup(this);
            wave.transform.parent = wavesParentComponent.transform;

            waves.Add(wave);
            return wave;
        }

        bool WaveActiveFilter(Wave wave) => wave.state == Wave.State.normal;
        public Wave ClosestBehind(float pos) => FindClosest.Behind(waves,pos,WaveActiveFilter);
        public Wave ClosestAhead(float pos) => FindClosest.Ahead(waves, pos, WaveActiveFilter);

        public override IChaser Chase(IChaseable c) => waveStarter.Chase(c);

        public override void ChaseableStartedLevel(IChaseable chaseable)
        {
            var xPos = chaseable.GetXPos();
            var waveBehind = ClosestBehind(xPos);

            if (waveBehind == null)
            {
                GetInactiveWave().StartWave();
            }
        }

        public override IChaser GetFirstBehindOrNew(float pos)
        {
            return GetFirstBehindOrNull(pos) ?? GetInactiveWave();
        }

        public override IChaser GetFirstBehindOrNull(float pos)
        {
            return ClosestBehind(pos);
        }
    }
}
