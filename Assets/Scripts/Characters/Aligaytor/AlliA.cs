using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliA : MonoBehaviour, IRespawnResetable {

    [SerializeField] float patrollRange;
    [SerializeField] float speed;
    [SerializeField] bool freindly = false;
    [SerializeField] Rigidbody2D rb;

    Vector2 spawnPos;
    float startSpeed;


    // Use this for initialization
    void Start () {
        spawnPos = transform.position;
        startSpeed = speed;
        GetComponent<SpriteRenderer>().flipX = true;
        GM.AddRespawnResetable(this);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        Vector2 pos = new Vector2(transform.position.x + speed, transform.position.y);
        rb.MovePosition(pos);
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


    public void PhillRespawned()
    {
        transform.position = spawnPos;
        speed = startSpeed;
    }
}
