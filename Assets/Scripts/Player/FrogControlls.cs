using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogControlls : MonoBehaviour {


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private Sprite restSprite;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode DebugKillKey = KeyCode.Q;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float jumpTimerBuff = 2;  
    [SerializeField] private float minJumpAmmount = 0.2f;

    private int layermask;
    private float jumpTimer = 0;
    private bool canJump = false;

    private Ray2D jumpRay;

    // Use this for initialization
    void Start () {
        
        layermask = LayerMask.GetMask("Ground");
	}
	
	// Update is called once per frame
	void Update () {

        if (canJump) { gameObject.GetComponent<SpriteRenderer>().sprite = restSprite; }
        else { gameObject.GetComponent<SpriteRenderer>().sprite = jumpSprite; }

        if (Input.GetKey(jumpKey))
        {
            jumpTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(jumpKey))
        {
            if (canJump)
            {
                jumpTimer *= jumpTimerBuff;
                jumpTimer = Mathf.Clamp(jumpTimer, minJumpAmmount, maxJumpTime);
                rb.AddForce(new Vector2(jumpForce * jumpTimer, jumpForce * jumpTimer));
            }
            jumpTimer = 0;
        }

        if (Input.GetKeyDown(DebugKillKey))
        {
            GetComponent<FrogManager>().KillPhill();
        }
    }

    private void FixedUpdate()
    {
        Vector2 rayPos1 = new Vector2(transform.position.x - 1.1f, transform.position.y);
        Vector2 rayPos2 = new Vector2(transform.position.x + 1.1f, transform.position.y);
        Vector2 rayPos3 = new Vector2(transform.position.x - 1.1f, transform.position.y - 1f);

        canJump = 
               Physics2D.Raycast(rayPos1, Vector2.down, 0.9f, layermask) 
            || Physics2D.Raycast(rayPos2, Vector2.down, 0.9f, layermask) 
            || Physics2D.Raycast(rayPos3, Vector2.right,2.2f , layermask);

        Debug.DrawRay(rayPos1, Vector2.down, Color.magenta, layermask);     Debug.DrawRay(rayPos2, Vector2.down, Color.magenta, layermask);
        Debug.DrawRay(rayPos3, new Vector2(2.2f,0), Color.magenta, layermask);
    }

}
