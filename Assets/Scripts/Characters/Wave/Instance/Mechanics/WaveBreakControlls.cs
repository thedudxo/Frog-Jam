using System.Collections;
using UnityEngine;
using FrogScripts;
using Chaseables;
using System.Linq;
using static WaveScripts.Wave.State;

namespace WaveScripts
{
    public class WaveBreakControlls : MonoBehaviour
    {
        [SerializeField] Wave wave;

        public float BreakPosition { get; private set; }
        Transform waveTransform;
        bool ReachedEndOfLevel => waveTransform.position.x > wave.level.region.end;

        private void Start()
        {
            waveTransform = wave.transform;
        }

        private void Update()
        {
            if (wave.state == Wave.State.normal)
            {
                if (ReachedEndOfLevel)
                {
                    BreakWave();
                }
            }
        }

        delegate bool Conditions(IChaseable chaseable, float chaseablePos);

        public void CheckIfWaveShouldBreak()
        {
            float wavePos = wave.GetXPos();
            Wave nextWave = wave.collection.ClosestAhead(wavePos);

            float nextWavePos;


            Conditions conditions;
            ChooseConditions();


            var FirstChaseableBeforeNextWave =
                (
                from chaseable in wave.collection.Chasing.GetAll()
                where conditions(chaseable, chaseable.GetXPos())
                select chaseable
                ).FirstOrDefault();

            if (FirstChaseableBeforeNextWave == null)
            {
                BreakWave();
            }


            void ChooseConditions()
            {
                if (nextWave == null)
                {
                    nextWavePos = wave.level.region.end + 1000;
                    conditions = NoChaseableAhead;
                }
                else
                {
                    nextWavePos = nextWave.GetXPos();
                    conditions = ChaseableAhead;
                }

                bool ChaseableAhead(IChaseable chaseable, float chaseablePos)
                {
                    return
                        NoChaseableAhead(chaseable, chaseablePos)
                        &&
                        chaseablePos < nextWavePos;
                }

                bool NoChaseableAhead(IChaseable chaseable, float chaseablePos)
                {
                    return
                        chaseable.IsCurrentlyChaseable
                        &&
                        wavePos < chaseablePos;
                }
            }
        }

        public void BreakWave()
        {
            if (wave.state != normal)
            {
                Debug.LogWarning($"Tried breaking wave in state '{wave.state}', expected normal", this);
                return;
            }

            BreakPosition = transform.position.x;
            wave.state = breaking;
        }

        public void StopBreaking()
        {
            if (wave.state != breaking)
            {
                Debug.LogWarning($"Tried to stop breaking on a wave whos state was '{wave.state}', expected breaking", this);
                return;
            }
            wave.state = inactive;
        }

    }
}