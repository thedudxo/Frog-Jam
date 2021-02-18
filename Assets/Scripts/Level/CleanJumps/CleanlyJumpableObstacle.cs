﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;
using UnityEngine.UI;

public class CleanlyJumpableObstacle : MonoBehaviour
{
    [SerializeField] FrogManager frogManager;

    [SerializeField] RememberCollisions[] rememberCollisions;

    [SerializeField] public GameObject templateCleanJumpEffect;


    private void Start()
    {
        foreach(Frog frog in frogManager.Frogs)
        {
            foreach(RememberCollisions remember in rememberCollisions)
            {
                remember.AddFrog(frog);
            }
        }
    }

    void CheckJump(Frog frog)
    {

        if (JumpWasClean())
        {
            frog.cleanJumpEffectsManager.DoCleanJumpEffect(this);
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
