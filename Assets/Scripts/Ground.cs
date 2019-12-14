using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject groundBro;

    private Vector2 spawnPos;

	// Use this for initialization
	void Start () {
        spawnPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.x - 10 > transform.position.x + (transform.localScale.x ))
        {
            NewGround();
        }

	}

    public void NewGround() //nonrelated to flash games
    {
        transform.position = new Vector2 (groundBro.transform.position.x + transform.localScale.x * 2.59f, transform.position.y);
        //transform.position = new Vector2 (groundBro.transform.position.x + (transform.localScale.x * 2 )+(transform.localScale.x * 0.5f), transform.position.y);
    }

    public void PhillDied()
    {
        transform.position = spawnPos;
    }
}

    

