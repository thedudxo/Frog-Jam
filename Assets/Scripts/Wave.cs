﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    [SerializeField] private float waveStartSpeed;
    [SerializeField] private float waveAcceleration; 
    //Waves really dont work this way but whatever
    //Maybie this wave has rocket thrusters on the back of it.

    public Vector2 spawnPosition;
    public float waveCurrentSpeed;

    // Use this for initialization
    void Start () {
        spawnPosition = transform.position;
        waveCurrentSpeed = waveStartSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + waveCurrentSpeed, 0);
        waveCurrentSpeed += waveAcceleration;
    }

    public void ResetWave()
    {
        waveCurrentSpeed = waveStartSpeed;
        transform.position = spawnPosition;
    }
}
