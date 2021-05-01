using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;
using UnityEngine.UI;

namespace LevelScripts
{
    public class CleanlyJumpableObstacle : MonoBehaviour
    {
        [SerializeField] Level level;
        FrogCollection frogManager;

        [SerializeField] RememberCollisions[] rememberCollisions;

        [SerializeField] public GameObject templateCleanJumpEffect;

        private void Awake()
        {
            frogManager = level.frogManager;
            level.cleanJumps.Add(this);
        }

        private void Start()
        {
            templateCleanJumpEffect.SetActive(false);

            foreach (Frog frog in frogManager.Frogs)
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
                    return frogManager.IDFrogs[collisionID];
                }
            }
        }
    }
}
