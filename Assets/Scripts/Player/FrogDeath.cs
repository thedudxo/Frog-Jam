using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrogDeath : MonoBehaviour {

    Vector2 spawnpoint;
    Rigidbody2D rb;
    GameObject wave;
    SpriteRenderer spriteRenderer;

    [SerializeField] float killPhillUnderY = -5;

    [Header("Respawns")]
    [SerializeField] float setBack = 25;
    [SerializeField] float waitToRespawn = 1;
    float currentRespawnWaitTime = -1; //negative when not dying currently
    bool reset;
    [SerializeField] GameObject colliders;

    [Header("Particles")]
    [SerializeField] ParticleSystem respawnParticles;
    int respawnEmit = 5;
    [SerializeField] ParticleSystem deathParticles;
    int deathEmit = 25;



    // Use this for initialization
    void Start() {
        FrogManager.frogDeath = this;
        FrogManager.frog = gameObject;
        wave = GM.currentLevel.wave;
        spawnpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update() {

        Debug.Log(GM.gameState);

        //wait for respawn
        if(! (currentRespawnWaitTime < 0)) //currently dying
        {
            currentRespawnWaitTime += Time.deltaTime;
            if(currentRespawnWaitTime >= waitToRespawn) {
                respawn(reset);
                currentRespawnWaitTime = -1; //not currently waiting
            }
        }
        else { //not dying
            if (transform.position.y < killPhillUnderY)
                { KillPhill(); }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wave") {
            KillPhill(true); }
    }


    void respawn(bool reset) //after pausing when dead
    {
        //setback
        if (!reset)
        {
            transform.position = new Vector2(transform.position.x - setBack, 5);
            wave.transform.position = new Vector2(wave.transform.position.x - setBack, wave.transform.position.y);
        }

        //respawn at platform
        if (transform.position.x < (spawnpoint.x + GM.currentLevel.spawnPlatformLength) || reset) //start from the beginning of the level
        {
            transform.position = spawnpoint;
            wave.GetComponent<Wave>().ResetWave();
            GM.splitManager.currentTime = 0;
        }

        //particles
        respawnParticles.gameObject.transform.position = transform.position;
        respawnParticles.Emit(respawnEmit);

        //these get disabled when killed
        rb.gravityScale = 1;
        spriteRenderer.enabled = true;
        colliders.SetActive(true);
        wave.GetComponent<Wave>().waveCurrentSpeed = wave.GetComponent<Wave>().waveStartSpeed;

        GM.audioManager.PlaySound("RespawnPop");
        rb.velocity = Vector3.zero;
        GM.gameState = GM.GameState.alive;
        GM.PhillRespawned();
    }

    public void KillPhill(bool reset = false)
    {
        if(GM.gameState == GM.GameState.finishedLevel) { return; }
        this.reset = reset;

        //these get changed back when respawning
        GM.gameState = GM.GameState.dead;
        rb.gravityScale = 0;
        spriteRenderer.enabled = false; 
        colliders.SetActive(false);
        wave.GetComponent<Wave>().waveCurrentSpeed = 0;

        rb.velocity = Vector3.zero;

        deathParticles.gameObject.transform.position = transform.position;
        deathParticles.Emit(deathEmit);

        GetComponent<FrogMetaBloodSplater>().startSplatter();
        GM.audioManager.PlaySound("DeathFart" + Random.Range(1,4));

        GM.PhillDied();
        currentRespawnWaitTime = 0;
    }
}
