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
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpTimerBuff = 2;
    [SerializeField] private float minJumpAmmount = 0.2f;

    private bool canJump = false;
    private float airTime = 0; //time since phill last touched something
    private float maxAnimationAirTime = 1.5f; // time where the landing animation gets maximum squish

    [Header("UI")]
    [SerializeField] Slider powerBar;

    [Header("Debug")]
    [SerializeField] bool drawRays = false;

    private int layermask;
    private float jumpKeyTime = 0;


    private float currentSpriteSwapTime = 0; //probably obsolete
    private readonly float spriteSwapMinWaitTime = .1f; //probably obsolete

    private Ray2D jumpRay;

    public bool CollidedSinceLastJump { get; private set; } = true;

    private void Awake()
    {
        FrogManager.frogControlls = this;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        layermask = LayerMask.GetMask("Ground");

        powerBar.minValue = minJumpAmmount;
        powerBar.maxValue = maxJumpTime;

        GM.AddRespawnResetable(this);
    }


    void Update() {

        if (GM.gameState != GM.GameState.alive) { return; }

        currentSpriteSwapTime += Time.deltaTime;

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

        powerBar.value = jumpKeyTime * jumpTimerBuff;


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

        //get a value between min/max ammount representing the strengh of the jump
        float jumpPower = jumpKeyTime * jumpTimerBuff;
        jumpPower = Mathf.Clamp(jumpPower, minJumpAmmount, maxJumpTime);

        //get a normalised version and use it to set the animation blend tree
        float jumpPowerNormalised = (jumpPower - minJumpAmmount) / (maxJumpTime - minJumpAmmount); //this is wrong probably
        Debug.Log(jumpPowerNormalised);
        animator.SetFloat("JumpPower", jumpPowerNormalised);

        if (Input.GetKeyUp(jumpKey))
        {
            animator.SetTrigger("ReleaseJump");
            animator.SetBool("ChargingJump", false);
            animator.SetFloat("JumpPowerAtKeyRelease", jumpPowerNormalised);
            if (canJump)
            {
                rb.AddForce(new Vector2(jumpForce * jumpPower, jumpForce * jumpPower));
                CollidedSinceLastJump = false;
            }
            jumpKeyTime = 0;
        }

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
