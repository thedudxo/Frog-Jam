using UnityEngine;
using Characters.Instances.Inputs;
using Frogs.Instances.State;

namespace Frogs.Instances.Inputs
{
    public class FrogSuicideInput : MonoBehaviour, ISuicideInput
    {
        [SerializeField] Frog frog;
        [SerializeField] public KeyCode key = KeyCode.Q;

        FrogStateContext context;

        public InputEvent suicideKeyEvent = new InputEvent();

        private void Start()
        {
            context = frog.controllers.stateContext;
        }

#if UNITY_ANDROID == false
        private void Update()
        {
            if (Input.GetKeyDown(key)) 
            {
                suicideKeyEvent.SetHoldingTrue(context.state);
            }
            if (Input.GetKeyUp(key))
            {
                suicideKeyEvent.SetHoldingFalse();
            }
        }
        #endif

        public void OnHold()
        {
            suicideKeyEvent.SetHoldingTrue(context.state);
        }

        public void OnRelease()
        {
            suicideKeyEvent.SetHoldingFalse();
        }

        public InputEvent GetSuicideInput()
        {
            return suicideKeyEvent;
        }
    }
}