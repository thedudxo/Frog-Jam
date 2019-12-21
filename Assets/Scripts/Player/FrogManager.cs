using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogManager : MonoBehaviour {

    Vector2 spawnpoint;
    Rigidbody2D rb;
    GameObject wave;

    [SerializeField] float resetDistance;
    [SerializeField] float setBack = 25;
    [SerializeField] float killPhillUnderY = -5;


    // Use this for initialization
    void Start () {
        GM.frogManager = this;
        wave = GM.currentLevel.wave;
        spawnpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < killPhillUnderY) {
            KillPhill(); }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wave"){
            KillPhill(true); }
    }



    public void KillPhill(bool reset = false)
    {

        if (!reset) 
        {
            transform.position = new Vector2(transform.position.x - setBack, 5);
            wave.transform.position = new Vector2(wave.transform.position.x - setBack, wave.transform.position.y);
        }
        

        if (transform.position.x < (spawnpoint.x + GM.currentLevel.spawnPlatformLength) || reset) //start from the beginning of the level
        {
            transform.position = spawnpoint;
            wave.GetComponent<Wave>().ResetWave();
        }
        
        rb.velocity = Vector3.zero;
        GM.PhillDied();

    }
}
