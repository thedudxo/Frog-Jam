using System.Collections;
using UnityEngine;
using static Utils.Normalise;

namespace Frogs.Instances.Jumps
{
    public class JumpAnimations : MonoBehaviour
    {
        [SerializeField] JumpController jumpController;
        [SerializeField] Animator animator;

        const float AirTimeMaxEffect = 1.5f;

        private void Update()
        {
            if (Input.GetKeyDown(jumpController.JumpKey))
            {
                animator.SetBool("ChargingJump", true);
            }
        }

        public void StartJump(float jumpPower)
        {
            animator.SetTrigger("ReleaseJump");
            animator.SetBool("ChargingJump", false);
            animator.SetFloat("JumpPowerAtKeyRelease", jumpPower);
        }

        public void Squish(float jumpPower)
        {
            animator.SetFloat("JumpPower", jumpPower);
        }

        public void Landed(float airTime)
        {
            float airTimeNormal = Normalise01(airTime, AirTimeMaxEffect);
            animator.SetFloat("AirTime", airTimeNormal);
        }

        public void SetGrounded(bool grounded)
        {
            animator.SetBool("Landed", grounded);
        }
    }
}