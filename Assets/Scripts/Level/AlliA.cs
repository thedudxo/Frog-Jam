﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliA : MonoBehaviour, IRespawnResetable {

    public float patrollRange;
    public float speed;
    private float startSpeed;
    public bool freindly = false;
    Vector2 spawnPos;
    

	// Use this for initialization
	void Start () {
        spawnPos = transform.position;
        startSpeed = speed;
        GetComponent<SpriteRenderer>().flipX = true;
        GM.AddRespawnResetable(this);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        if (speed > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            if (transform.position.x > patrollRange + spawnPos.x)
            {
                speed *= -1;
                
            }
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (transform.position.x < -patrollRange + spawnPos.x)
            {
                speed *= -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Phill" && !freindly)
        {
            FrogManager.frogDeath.KillPhill();
            Statistics.aligatorDeaths++;
        }
    }


    public void RespawnReset()
    {
        transform.position = spawnPos;
        speed = startSpeed;
    }
}
