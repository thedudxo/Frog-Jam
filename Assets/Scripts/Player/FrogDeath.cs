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
    [SerializeField] public float respawnSetBack = 25;
    [SerializeField] float waitToRespawn = 1;
    float currentRespawnWaitTime = -1; //negative when not dying currently
    bool reset;
    //[SerializeField] Collider collider;

    [Header("Particles")]
    [SerializeField] ParticleSystem respawnParticles;
    int respawnEmit = 5;
    [SerializeField] ParticleSystem deathParticles;
    int deathEmit = 25;



    private void Awake()
    {
        FrogManager.frogDeath = this;
        FrogManager.frog = gameObject;
    }

    // Use this for initialization
    void Start() {

        wave = GM.currentLevel.wave;
        spawnpoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update() {

        //wait for respawn
        if(! (currentRespawnWaitTime < 0)) //currently dying
        {
            currentRespawnWaitTime += Time.deltaTime;
            if(GM.gameMusic.IsBeatFrame && currentRespawnWaitTime >= waitToRespawn) {
                Respawn(reset);
                currentRespawnWaitTime = -1; //not currently waiting
            }
        }
        else { //not dying
            if (transform.position.y < killPhillUnderY && GM.gameMusic.IsBeatFrame)
                { KillPhill(); Statistics.waterDeaths++; }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wave") {
            KillPhill(true); Statistics.waveDeaths++; }
    }


    void Respawn(bool reset) //after pausing when dead
    {
        //setback
        if (!reset)
        {
            transform.position = new Vector2(transform.position.x - respawnSetBack, 5);
            wave.transform.position = new Vector2(wave.transform.position.x - respawnSetBack, wave.transform.position.y);
        }

        //respawn at platform
        if (transform.position.x < (spawnpoint.x + GM.currentLevel.spawnPlatformLength) || reset) 
            //start from the beginning of the level
        {
            transform.position = spawnpoint;
            wave.GetComponent<Wave>().ResetWave();
            GM.splitManager.currentTime = 0;
            GM.currentLevel.FrogRespawned();
        }

        //particles
        respawnParticles.gameObject.transform.position = transform.position;
        respawnParticles.Emit(respawnEmit);

        //these get disabled when killed
        rb.gravityScale = 1;
        spriteRenderer.enabled = true;
        GetComponent<PolygonCollider2D>().enabled = true;
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
        GetComponent<PolygonCollider2D>().enabled = false;
        //those get changed back when respawning

        rb.velocity = Vector3.zero;

        deathParticles.gameObject.transform.position = transform.position;
        deathParticles.Emit(deathEmit);

        GetComponent<FrogMetaBloodSplater>().startSplatter();
        GM.audioManager.PlaySound("DeathFart" + Random.Range(1,4));
        GM.gameMusic.DetuneMusic();

        GM.PhillDied();
        Statistics.totalDeaths++;
        currentRespawnWaitTime = 0;
    }
}
