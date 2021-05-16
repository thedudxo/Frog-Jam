using UnityEngine;
using Frogs.Instances.Jump;

namespace Frogs.Instances.Inputs
{
    public class Jump : MonoBehaviour, INotifyOnAnyRespawn
    {
        [SerializeField] Frog frog;
        [SerializeField] public KeyCode key;
        [SerializeField] JumpController jumpController;
        float chargeTime = 0;

        private void Start()
        {
            frog.events.SubscribeOnAnyRespawn(this);
        }

        private void Update()
        {
#if UNITY_ANDROID
#else

            if (Input.GetKey(key))
            {
                HoldingInput();
            }

            if (Input.GetKeyUp(key))
            {
                ReleasedInput();
            }
#endif
        }

        private void ReleasedInput()
        {
            chargeTime = 0;
            jumpController.AttemptJump();
            jumpController.SetJumpCharge(0);
        }

        private void HoldingInput()
        {
            chargeTime += Time.deltaTime;
            jumpController.SetJumpCharge(chargeTime);
        }

        public void OnAnyRespawn()
        {
            chargeTime = 0;
        }




    }
}