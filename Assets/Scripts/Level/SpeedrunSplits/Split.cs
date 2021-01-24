using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts.UI;

namespace LevelScripts
{
    public class Split : MonoBehaviour
    {
        [SerializeField] public string Name;
        [SerializeField] ParticleSystem newPBParticles;
        [SerializeField] SplitManager splitManager;

        List<FrogSplitTracker> splitUIs;

        public void AddSplitUI(FrogSplitTracker splitUI)
        {
            splitUIs.Add(splitUI);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool collisionIsPlayer = collision.gameObject.tag == GM.playerTag;

            if (collisionIsPlayer)
            {
                foreach (FrogSplitTracker splitUI in splitUIs)
                {
                    splitUI.ReachedSplit();
                }
            }
        }

        public void EmitParticles()
        {
            const int ParticleEmitAmmount = 20;
            newPBParticles.Emit(ParticleEmitAmmount);
        }



        public string GetSplitName()
        {
            return Name;
        }

        public float GetBestTime()
        {
            return bestTime;
        }
    }
}
