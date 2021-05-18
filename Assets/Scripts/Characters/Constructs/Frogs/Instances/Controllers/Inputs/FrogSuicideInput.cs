using UnityEngine;
using Characters.Instances.Deaths;
using Frogs.Instances.State;

namespace Frogs.Instances.Inputs
{
    public class FrogSuicideInput : MonoBehaviour, ISuicideInput
    {
        [SerializeField] Frog frog;
        [SerializeField] public KeyCode key = KeyCode.Q;

        [HideInInspector] public bool holding = false;

        private void Update()
        {
            if (Input.GetKey(key))
            {
                //holding = true;
            }
            else
            {
                //holding = false;
            }
        }

        public void Test()
        {
            Debug.Log("got it");
        }

        public void OnHold()
        {
            holding = true;
        }

        public void OnRelease()
        {
            holding = false;
        }

        public bool GetSuicideInput()
        {
            return holding;
        }
    }
}