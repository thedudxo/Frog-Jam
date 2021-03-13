using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts.Jump
{
    public class JumpController : MonoBehaviour, INotifyOnAnyRespawn
    {
        KeyCode jumpKey;
        [SerializeField] Animator animator;
        [SerializeField] Frog frog;
        [SerializeField] Rigidbody2D rb;

        [Header("Components")]
        [SerializeField] GroundedDetection groundedDetection;

        [Header("Audio")]
        [SerializeField] AudioClip landSounds;
        [SerializeField] AudioClip jumpSounds;

        [Header("Sprites")]
        public SpriteRenderer spriteRenderer;
        public Sprite jumpSprite;
        public Sprite restSprite;

        [Header("UI")]
        public Slider powerBar;

        [SerializeField] float jumpForce = 1000;
        [SerializeField] float horizontalJumpForce = 300;
              float jumpKeyTime = 0; //how long the jump key has been held down
        const float maxJumpKeyTime = .22f;  //how long the key must be heled to get max power
              float jumpTimeNormalised = 0; // how long the key was held 0 to 1
        const float minJumpTimeNormalised = .15f; //the smallest jump you can make
        const float jumpKeyTimeMinThreshold = 0.3f; //if jump key is heled for less than this time jump will be minimum power 

        bool canJump = false;
        float airTime = 0; //time since phill last touched something
        float maxAnimationAirTime = 1.5f; // time where the landing animation gets maximum squish

        public bool CollidedSinceLastJump { get; private set; } = true;

        void Start()
        {
            powerBar.minValue = 0;
            powerBar.maxValue = 1;

            jumpKey = frog.controlls.jumpKey;

            frog.events.SubscribeOnAnyRespawn(this);
        }

        bool IsGrounded => groundedDetection.IsGrounded;

        void Update()
        {
            //can the frog jump?
            if (IsGrounded)
            {
                if (canJump == false)
                { // the frame where phill landed
                    LandedAnimation();
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
            { spriteRenderer.sprite = restSprite; }
            else
            { spriteRenderer.sprite = jumpSprite; }

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

                jumpSounds.GetRandomAudioSource().Play();

                if (canJump)
                {
                    //if jump key is heled for less than this time jump will be minimum power
                    //increases accuracy when player intends to make small jumps
                    if ((jumpTimeNormalised < jumpKeyTimeMinThreshold))
                    {
                        jumpTimeNormalised = minJumpTimeNormalised;
                    }

                    rb.AddForce(new Vector2(horizontalJumpForce * jumpTimeNormalised, jumpForce * jumpTimeNormalised));
                    rb.AddTorque(-45);
                    CollidedSinceLastJump = false;
                }

                jumpKeyTime = 0;
            }

            //get normalised jump time
            jumpTimeNormalised = Mathf.Clamp((jumpKeyTime / maxJumpKeyTime), 0, 1);

            animator.SetFloat("JumpPower", jumpTimeNormalised);
            powerBar.value = jumpTimeNormalised;
        }

        private void LandedAnimation()
        {
            float airTimeNormalised = Mathf.Clamp(airTime, 0, maxAnimationAirTime) / maxAnimationAirTime;
            animator.SetFloat("AirTime", airTimeNormalised);
            airTime = 0;

            landSounds.GetRandomAudioSource().Play();
        }

        public void OnCollisionEnter2D()
        {
            CollidedSinceLastJump = true;
        }

        public void OnAnyRespawn()
        {
            jumpKeyTime = 0;
        }
    }
}
