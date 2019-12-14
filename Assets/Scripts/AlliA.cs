using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliA : MonoBehaviour {

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
        GM.AddGater(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
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
            collision.gameObject.GetComponent<FrogManager>().KillPhill();
        }
    }


    public void ResetGater()
    {
        transform.position = spawnPos;
        speed = startSpeed;
    }
}
