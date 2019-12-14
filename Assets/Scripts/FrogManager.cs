using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogManager : MonoBehaviour {

    private Vector2 spawnpoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject wave;
    [SerializeField] private GameObject deathcounter;
    [SerializeField] private float resetDistance;
    [SerializeField] private float setBack = 25;

    private int deaths = 0;

    // Use this for initialization
    void Start () {
        spawnpoint = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -5)
        {
            KillPhill();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wave")
        {
            KillPhill(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathCount")
        {
            deaths++;
            deathcounter.GetComponent<TextMesh>().text = "Deaths Here: " + deaths;
        }
    }

    public void KillPhill(bool reset = false)
    {

        if (!reset)
        {
            transform.position = new Vector2(transform.position.x - setBack, 5);
            wave.transform.position = new Vector2(wave.transform.position.x - setBack, wave.transform.position.y);
        }
        

        if (transform.position.x < spawnpoint.x || reset)
        {
            transform.position = spawnpoint;
            wave.GetComponent<Wave>().ResetWave();
        }
        
        rb.velocity = Vector3.zero;
        GM.instance.PhillDied();

    }
}
