using UnityEngine;
using Frogs.Instances.Jumps;
using Frogs.Instances.Setups;

namespace Frogs.Instances.Inputs
{
    public class JumpInput : MonoBehaviour, INotifyOnAnyRespawn
    {
        [SerializeField] Frog frog;
        [SerializeField] JumpController jumpController;
        float chargeTime = 0;

        int touches = 0;

        delegate bool TouchCheck(Touch touch);
        TouchCheck ScreenTouchCheck;

        private void Start()
        {
            frog.events.SubscribeOnAnyRespawn(this);

            switch (frog.ViewMode)
            {
                case ViewMode.SplitTop:
                    ScreenTouchCheck = TouchedTopHalf;
                    break;

                case ViewMode.SplitBottom:
                    ScreenTouchCheck = TouchedBottomHalf;
                    break;

                case ViewMode.Single:
                    ScreenTouchCheck = NoCheck;
                    break;
            }
        }

        bool TouchedTopHalf(Touch touch)
        {
            return touch.position.y > Screen.height / 2;
        }

        bool TouchedBottomHalf(Touch touch)
        {
            return touch.position.y < Screen.height / 2;
        }

        bool NoCheck(Touch touch) => true;



        private void Update()
        {
#if UNITY_ANDROID

            foreach(Touch touch in Input.touches)
            {
                if (ScreenTouchCheck(touch)){

                    if (touch.phase == TouchPhase.Began)
                    {
                        touches++;
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        touches--;
                        if (touches == 0)
                        {
                            ReleasedInput();
                        }
                    }

                    if (touches > 0)
                    {
                        HoldingInput();
                    }
                }
            }
#else
            KeyCode key = frog.controllers.input.GetKeybind(Action.Jump);

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