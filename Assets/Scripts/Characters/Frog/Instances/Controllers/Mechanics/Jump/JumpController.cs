using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Util.Normalise;

namespace Frogs.Jump
{
    public class JumpController : MonoBehaviour, INotifyOnAnyRespawn
    {
        [Header("Components")]
        [SerializeField] Frog frog;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] GroundedDetection groundedDetection;
        [SerializeField] JumpAnimations animations;

        [Header("Audio")]
        [SerializeField] AudioClip landSounds;
        [SerializeField] AudioClip jumpSounds;

        [Header("UI")]
        public Slider powerBar;

        [Header("Parameters")]
        [SerializeField] Vector2 maxJumpForce = new Vector2(600,500);

        public KeyCode JumpKey => frog.controllers.input.jump.key;

        float jumpChargeTime = 0;
        const float maxJumpCharge = .22f;
              float jumpCharge01 = 0;
        const float minJumpCharge01 = .15f; 
        const float minJumpChargeThreshhold01 = 0.3f; 

        bool _canJump = false;
        bool CanJump { 
            get { return _canJump; } 
            set 
            {
                bool changedToTrue = (value != _canJump) && value;
                _canJump = value;
                if (changedToTrue) LandingFrame();
            } 
        }
        float airTime = 0; 

        public bool CollidedSinceLastJump { get; private set; } = true;

        void Start()
        {
            powerBar.minValue = 0;
            powerBar.maxValue = 1;

            frog.events.SubscribeOnAnyRespawn(this);
        }

        void Update()
        {
            CanJump = groundedDetection.IsGrounded;

            if (CanJump == false) airTime += Time.deltaTime;

            animations.SetGrounded(CanJump);


            if (Input.GetKey(JumpKey))
            {
                jumpChargeTime += Time.deltaTime;
            }

            if (Input.GetKeyUp(JumpKey)) AttemptJump();

            jumpCharge01 = Normalise01(jumpChargeTime, maxJumpCharge);

            animations.Squish(jumpCharge01);
            powerBar.value = jumpCharge01;
        }

        private void AttemptJump()
        {
            animations.StartJump(jumpCharge01);

            jumpSounds.GetRandomAudioSource().Play();

            if (CanJump) Jump();

            jumpChargeTime = 0;


            void Jump()
            {
                IncreaseSmallJumpAccuracy();
                Vector2 jumpForce = CalculateFinalJumpForce();
                PerformJump(jumpForce);

                void IncreaseSmallJumpAccuracy()
                {
                    bool minimumJump = jumpCharge01 < minJumpChargeThreshhold01;
                    if (minimumJump) jumpCharge01 = minJumpCharge01;
                }

                Vector2 CalculateFinalJumpForce()
                {
                    return new Vector2(
                        maxJumpForce.x * jumpCharge01,
                        maxJumpForce.y * jumpCharge01
                        );
                }

                void PerformJump(Vector2 force)
                {
                    rb.AddForce(force);
                    rb.AddTorque(-45);
                    CollidedSinceLastJump = false;
                }
            }
        }

        private void LandingFrame()
        {
            animations.Landed(airTime);
            landSounds.GetRandomAudioSource().Play();

            airTime = 0;
        }

        public void OnCollisionEnter2D()
        {
            CollidedSinceLastJump = true;
        }

        public void OnAnyRespawn()
        {
            jumpChargeTime = 0;
        }
    }
}
