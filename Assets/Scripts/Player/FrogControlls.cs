using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogControlls : MonoBehaviour, IRespawnResetable {


    Rigidbody2D rb;
    [Header("Sprites")]
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite restSprite;
    [SerializeField] private GameObject frogSpriteObject;

    [Header("Controlls")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode DebugKillKey = KeyCode.Q;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("Physics")]
    private float jumpForce = 400; 
    private float jumpKeyTime = 0; //how long the jump key has been held down
    private float maxJumpKeyTime = .22f;  //how long the key must be heled to get max power
    float jumpTimeNormalised = 0; // how long the key was held 0 to 1
    private float minJumpTimeNormalised = .18f; //the smallest jump you can make
    private float jumpKeyTimeMinThreshold = 0.3f; //if jump key is heled for less than this time jump will be minimum power


    private bool canJump = false;
    private float airTime = 0; //time since phill last touched something
    private float maxAnimationAirTime = 1.5f; // time where the landing animation gets maximum squish

    [Header("UI")]
    [SerializeField] Slider powerBar;

    [Header("Debug")]
    [SerializeField] bool drawRays = false;

    private int layermask;

    private Ray2D jumpRay;

    public bool CollidedSinceLastJump { get; private set; } = true;

    private void Awake()
    {
        FrogManager.frogControlls = this;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        layermask = LayerMask.GetMask("Ground");

        powerBar.minValue = 0;
        powerBar.maxValue = 1;

        GM.AddRespawnResetable(this);
    }


    void Update() {

        if (GM.gameState != GM.GameState.alive) { return; }

        //can the frog jump?
        if (rb.velocity.magnitude <= Vector2.zero.magnitude + 2f) // not moving too fast
            {
            if(canJump == false){ // the frame where phill landed
                //landing animation
                float airTimeNormalised = Mathf.Clamp(airTime, 0, maxAnimationAirTime) / maxAnimationAirTime;
                animator.SetFloat("AirTime", airTimeNormalised);
                airTime = 0;
                }
            canJump = true;
            }
        else
            {
            canJump = false;
            airTime += Time.deltaTime;
            }
        animator.SetBool("Landed", canJump);




        if (canJump)
            { frogSpriteObject.GetComponent<SpriteRenderer>().sprite = restSprite;}
        else
            { frogSpriteObject.GetComponent<SpriteRenderer>().sprite = jumpSprite;}

        if (Input.GetKeyDown(jumpKey))
        {
            animator.SetBool("ChargingJump", true);
        }



        if (Input.GetKey(jumpKey))
            {
            jumpKeyTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(jumpKey))
        {
            //do jump
            animator.SetTrigger("ReleaseJump");
            animator.SetBool("ChargingJump", false);
            animator.SetFloat("JumpPowerAtKeyRelease", jumpTimeNormalised);

            if (canJump) 
            {
                //if jump key is heled for less than this time jump will be minimum power
                //increases accuracy when player intends to make small jumps
                if ((jumpTimeNormalised < jumpKeyTimeMinThreshold))
                {
                    jumpTimeNormalised = minJumpTimeNormalised;
                }

                rb.AddForce(new Vector2(jumpForce * jumpTimeNormalised, jumpForce * jumpTimeNormalised));
                CollidedSinceLastJump = false;
            }

            jumpKeyTime = 0;
        }

        //get normalised jump time
        jumpTimeNormalised = Mathf.Clamp((jumpKeyTime / maxJumpKeyTime), 0, 1);

        animator.SetFloat("JumpPower", jumpTimeNormalised);
        powerBar.value = jumpTimeNormalised;

        if (Input.GetKeyDown(DebugKillKey))
        {
            FrogManager.frogDeath.KillPhill();
            Statistics.suicideDeaths++;
        }

        //quit to menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GM.QuitToMenu();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollidedSinceLastJump = true;
    }

    private void FixedUpdate()
    {

        if (drawRays)
        {
            Vector2 rayPos1 = new Vector2(transform.position.x - 1.1f, transform.position.y);
            Vector2 rayPos2 = new Vector2(transform.position.x + 1.1f, transform.position.y);
            Vector2 rayPos3 = new Vector2(transform.position.x - 1.1f, transform.position.y - 1f);

            Debug.DrawRay(rayPos1, Vector2.down, Color.magenta, layermask); Debug.DrawRay(rayPos2, Vector2.down, Color.magenta, layermask);
            Debug.DrawRay(rayPos3, new Vector2(2.2f, 0), Color.magenta, layermask);
        }

    }

    public void PhillRespawned()
    {
        jumpKeyTime = 0;
    }
}
