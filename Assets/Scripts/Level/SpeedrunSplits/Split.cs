using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;

namespace LevelScripts
{
    public class Split : MonoBehaviour
    {
        [SerializeField] public string Name { get; private set; }
        [SerializeField] ParticleSystem newPBParticles;
        [SerializeField] SplitManager splitManager;
        [SerializeField] Canvas playerCopyCanvas;

        List<FrogSplitTracker> splitUIs = new List<FrogSplitTracker>();


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

        public void EmitNewPBParticles()
        {
            const int ParticleEmitAmmount = 20;
            newPBParticles.Emit(ParticleEmitAmmount);
        }

        public bool IsPastSplit(float Xposition)
        {
            float splitXPos = transform.position.x;

            return Xposition > splitXPos;
        }

    }
}
