using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogControlls : MonoBehaviour {


    Rigidbody2D rb;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite restSprite;

    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode DebugKillKey = KeyCode.Q;

    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpTimerBuff = 2;  
    [SerializeField] private float minJumpAmmount = 0.2f;

    [SerializeField] bool drawRays = false;

    [SerializeField] Slider powerBar;

    private int layermask;
    private float jumpKeyTime = 0;

    private bool canJump = false;
    private float currentSpriteSwapTime = 0;
    private readonly float spriteSwapMinWaitTime = .1f;

    private Ray2D jumpRay;

    public bool CollidedSinceLastJump { get; private set; } = true;

    private void Awake()
    {
        FrogManager.frogControlls = this;
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        layermask = LayerMask.GetMask("Ground");

        powerBar.minValue = minJumpAmmount;
        powerBar.maxValue = maxJumpTime;
	}


	void Update () { 

        if(GM.gameState != GM.GameState.alive) { return; }

        currentSpriteSwapTime += Time.deltaTime;

        //can the frog jump?
        if (rb.velocity.magnitude <= Vector2.zero.magnitude + 2f) // not moving too fast
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        //currentSpriteSwapTime += Time.deltaTime;
        //if(currentSpriteSwapTime > spriteSwapMinWaitTime)
        //{
        //    
        //    currentSpriteSwapTime = 0;
        //}

        powerBar.value = jumpKeyTime * jumpTimerBuff;


        if (canJump) {
            gameObject.GetComponent<SpriteRenderer>().sprite = restSprite;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().sprite = jumpSprite;

        }

        if (Input.GetKey(jumpKey))
        {
            jumpKeyTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(jumpKey))
        {
            if (canJump)
            {
                jumpKeyTime *= jumpTimerBuff;
                jumpKeyTime = Mathf.Clamp(jumpKeyTime, minJumpAmmount, maxJumpTime);
                rb.AddForce(new Vector2(jumpForce * jumpKeyTime, jumpForce * jumpKeyTime));
                CollidedSinceLastJump = false;
                
            }
            jumpKeyTime = 0;
        }

        if (Input.GetKeyDown(DebugKillKey))
        {
            FrogManager.frogDeath.KillPhill();
            Statistics.suicideDeaths++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollidedSinceLastJump = true;

        //colisions++;
        //if (colisions > 0) { canJump = true; }
        //Debug.Log(colisions);
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    colisions--;
    //    if (colisions == 0) { canJump = false; }
    //    Debug.Log(colisions);
    //}

    private void FixedUpdate()
    {

        Vector2 rayPos1 = new Vector2(transform.position.x - 1.1f, transform.position.y);
        Vector2 rayPos2 = new Vector2(transform.position.x + 1.1f, transform.position.y);
        Vector2 rayPos3 = new Vector2(transform.position.x - 1.1f, transform.position.y - 1f);

        //canJump = 
        //       Physics2D.Raycast(rayPos1, Vector2.down, 0.9f, layermask) 
        //    || Physics2D.Raycast(rayPos2, Vector2.down, 0.9f, layermask) 
        //    || Physics2D.Raycast(rayPos3, Vector2.right,2.2f , layermask);

        if (drawRays)
        {
            Debug.DrawRay(rayPos1, Vector2.down, Color.magenta, layermask); Debug.DrawRay(rayPos2, Vector2.down, Color.magenta, layermask);
            Debug.DrawRay(rayPos3, new Vector2(2.2f, 0), Color.magenta, layermask);
        }
        
    }

}
