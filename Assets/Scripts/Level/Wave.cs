using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;
using LevelScripts;

public class Wave : MonoBehaviour , INotifyOnRestart, INotifyOnSetback
{

    [SerializeField] float waveApproachingMusicMaxDistance = 5;
    [SerializeField] Level level;
    FrogManager frogManager;

    Vector2 spawnPosition;
    const float speed = 8f;

    const float restartWaitSeconds = 1;
    float restartWaitTimer = 0;
    bool restarting = false;

    Frog subscribedToRestart;

    void Start () {
        spawnPosition = transform.position;

        SubscribeToFrogs();

        void SubscribeToFrogs()
        {
            frogManager = level.frogManager;

            foreach (Frog frog in frogManager.Frogs)
            {
                frog.SubscribeOnSetback(this);
            }
        }
    }

    bool ReachedEndOfLevel => transform.position.x > level.end;

    private void Update()
    {
        if (restarting)
        {
            restartWaitTimer += Time.deltaTime;
            if (restartWaitTimer > restartWaitSeconds)
            {
                restarting = false;
                restartWaitTimer = 0;
                transform.position = spawnPosition;
            }
        }


        if (ReachedEndOfLevel)
        {
            OnRestart();
        }
    }

    private void FixedUpdate()
    {
        MoveWave();

        void MoveWave()
        {
            transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Frog hitFrog = frogManager.GetFrogFromGameobject(collision.gameObject);
        if (hitFrog!= null)
            checkIfLastFrogRemaining();

        void checkIfLastFrogRemaining()
        {
            bool hitLastRemaningFrog = frogManager.FrogIsFirst(hitFrog);
            if (hitLastRemaningFrog)
            {
                hitFrog.SubscribeOnRestart(this);
                subscribedToRestart = hitFrog;
            }
        }
    }

    public void OnRestart()
    {
        restarting = true;
        if (subscribedToRestart != null)
        {
            subscribedToRestart.UnsubscribeOnRestart(this);
            subscribedToRestart = null;
        }
    }

    public void OnSetback()
    {
        return;
        float setBackXPos = transform.position.x - GM.respawnSetBack;
        transform.position = new Vector2 (setBackXPos, transform.position.y);
    }
}
