using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    [SerializeField] float waveApproachingMusicMaxDistance = 5;
    bool waveIsClose = false;

    Vector2 spawnPosition;
   const float speed = 8f;

    // Use this for initialization
    void Start () {
        spawnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
    }


    public void Setback(float ammount)
    {
        transform.position = new Vector2 (transform.position.x - ammount, transform.position.y);
    }

    public void Restart()
    { 
        transform.position = spawnPosition;
    }
}
