using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs.Collections;
using Frogs.Instances;
using UnityEngine.UI;

namespace Levels
{
    public class CleanlyJumpableObstacle : MonoBehaviour
    {
        [SerializeField] Level level;
        FrogCollection frogs;

        [SerializeField] RememberCollisions[] rememberCollisions;

        [SerializeField] public GameObject templateCleanJumpEffect;

        private void Awake()
        {
            frogs = level.frogManager;
            level.cleanJumps.Add(this);
        }

        private void Start()
        {
            templateCleanJumpEffect.SetActive(false);

            foreach (Frog frog in frogs.Frogs)
            {
                foreach (RememberCollisions remember in rememberCollisions)
                {
                    remember.AddFrog(frog);
                }
            }
        }

        void CheckJump(Frog frog)
        {

            if (JumpWasClean())
            {
                frog.controllers.cleanJumpEffects.DoCleanJumpEffect(this);
            }

            bool JumpWasClean()
            {
                bool cleanJump = true;

                foreach (RememberCollisions remember in rememberCollisions)
                {
                    if (remember.FrogsCollided[frog])
                    {
                        cleanJump = false;
                    }
                }

                return cleanJump;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool isPlayer = collision.gameObject.tag == GM.playerTag;

            if (isPlayer)
            {
                CheckJump(GetFrog());

                Frog GetFrog()
                {
                    int collisionID = collision.gameObject.GetInstanceID();
                    return frogs.IDFrogs[collisionID];
                }
            }
        }
    }
}
