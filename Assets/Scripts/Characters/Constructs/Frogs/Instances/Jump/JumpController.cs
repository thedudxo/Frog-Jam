using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Util.Normalise;

namespace Frogs.Instances.Jumps
{
    public class JumpController : MonoBehaviour
    {
        [Header("Dependancies")]
        [SerializeField] Frog frog;
        [SerializeField] Rigidbody2D rb;

        [Header("Components")]
        [SerializeField] GroundedDetection groundedDetection;
        [SerializeField] JumpAnimations animations;
        Jumper jumper;

        [Header("Audio")]
        [SerializeField] AudioClip landSounds;
        [SerializeField] AudioClip jumpSounds;

        [Header("UI")]
        [SerializeField] Slider powerBar;

        public KeyCode JumpKey => frog.controllers.input.GetKeybind(Inputs.Action.Jump);


        const float maxJumpCharge = .22f;
              float jumpCharge01 = 0;

        bool _canJump = false;
        bool Grounded { 
            get { return _canJump; } 
            set 
            {
                bool changedToTrue = (value != _canJump) && value == true;
                _canJump = value;
                if (changedToTrue) LandingFrame();
            } 
        }
        float airTime = 0; 

        void Start()
        {
            powerBar.minValue = 0;
            powerBar.maxValue = 1;

            jumper = new Jumper(new RigidBody2DForceReceiver(rb));
        }

        void Update()
        {
            Grounded = groundedDetection.IsGrounded;

            if (Grounded == false) 
                airTime += Time.deltaTime;

            animations.SetGrounded(Grounded);
            animations.Squish(jumpCharge01);

            powerBar.value = jumpCharge01;
        }

        public void SetJumpCharge(float chargeTime)
        {
            jumpCharge01 = Normalise01(chargeTime, maxJumpCharge);
        }

        public void AttemptJump()
        {
            bool frogNotAlive = frog.controllers.stateContext.state != frog.controllers.stateContext.alive;
            if (frogNotAlive) return;

            animations.StartJump(jumpCharge01);

            jumpSounds.GetRandomAudioSource().Play();

            if (Grounded) jumper.Jump(jumpCharge01);
        }

        private void LandingFrame()
        {
            animations.Landed(airTime);
            landSounds.GetRandomAudioSource().Play();

            airTime = 0;
        }
    }
}
