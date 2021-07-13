using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Utils.Normalise;
using Frogs.Instances.State;

namespace Frogs.Instances.Jumps
{
    interface IGroundedDetection
    {
        bool IsGrounded();
    }

    public class JumpController : MonoBehaviour
    {
        [Header("Dependancies")]
        [SerializeField] Frog frog;
        FrogStateContext stateContext;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] Slider powerBarUiSlider;

        [Header("Components")]
        [SerializeField] GroundedDetection groundedDetection;
        [SerializeField] JumpAnimations animations;
        Jumper jumper;
        JumpPowerBar jumpPowerBar;
        JumpAudio jumpAudio;

        [Header("Audio")]
        [SerializeField] AudioClip landSounds;
        [SerializeField] AudioClip jumpSounds;

        const float maxJumpCharge = .22f;
              float jumpCharge01 = 0;

        bool grounded = false;
        bool groundedLastFrame;
        float airTime = 0; 

        void Start()
        {
            jumpAudio = new JumpAudio (landSounds, jumpSounds);
            jumpPowerBar = new JumpPowerBar(powerBarUiSlider);
            stateContext = frog.controllers.stateContext;

            List<IJump01Modifier> modifiers = new List<IJump01Modifier>()
            {
                new IncreaseSmallJumpAccuracy(0.15f, 0.3f)
            };

            jumper = new Jumper(new RigidBody2DForceReceiver(rb), modifiers);
        }

        void Update()
        {
            if (CheckGrounded() == false)
                AddAirTime();

            animations.SetValues(grounded, jumpCharge01);

            jumpPowerBar.SetValue(jumpCharge01);
        }

        private bool CheckGrounded()
        {
            groundedLastFrame = grounded;
            grounded = groundedDetection.IsGrounded;

            CheckForLandingFrame(groundedLastFrame);

            return grounded;
        }

        private void CheckForLandingFrame(bool groundedLastFrame)
        {
            bool JustLanded = groundedLastFrame == false && grounded == true;
            if (JustLanded) LandingFrame();
        }

        private void AddAirTime()
        {
            airTime += Time.deltaTime;
        }

        public void SetJumpCharge(float chargeTime)
        {
            jumpCharge01 = Normalise01(chargeTime, maxJumpCharge);
        }

        bool Dead => stateContext.state != stateContext.alive;
        public void AttemptJump()
        {
            if (Dead) return;

            CharacterJumpEffects();

            if (grounded)
                DoJump();
        }

        private void CharacterJumpEffects()
        {
            animations.StartJump(jumpCharge01);
            jumpAudio.PlayJump();
        }

        private void DoJump()
        {
            jumper.Jump(jumpCharge01);
        }

        private void LandingFrame()
        {
            animations.Landed(airTime);
            jumpAudio.PlayLand();

            airTime = 0;
        }
    }
}
