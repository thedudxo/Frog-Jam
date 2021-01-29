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
        [SerializeField] public Canvas playerCopyCanvas;
        [SerializeField] Text title;

        List<ISplitReferencer> references = new List<ISplitReferencer>();


        private void Start()
        {
            playerCopyCanvas.gameObject.SetActive(false);
            Name = title.text;
        }

        public void AddSplitReferencer(ISplitReferencer splitReferencer)
        {
            references.Add(splitReferencer);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            /*
             * find which player just collided
             * notify that player
             */

            GameObject obj = collision.gameObject;

            bool collisionIsPlayer = obj.tag == GM.playerTag;

            if (collisionIsPlayer)
            {
                foreach (ISplitReferencer splitUI in references)
                {
                    if (obj.GetComponent<Frog>() == splitUI.Frog)
                    {
                        splitUI.ReachedSplit();
                        break;
                    }
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
