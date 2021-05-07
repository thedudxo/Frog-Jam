using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public class Suicide : MonoBehaviour
    {
        [SerializeField] public KeyCode key = KeyCode.Q;
        //[SerializeField] someUiController ui;
        //[SerializeField] someInputResult result;
        float holdToRestartTime = 1f;
        float keyHeldTime = 0f;

        private void Update()
        {
            if (Input.GetKey(key))
            {
                float keyHeldNormal = Util.Normalise.Normalise01(keyHeldTime, holdToRestartTime);
                //update UI

                if (keyHeldTime >= holdToRestartTime)
                {
                    keyHeldTime = 0;
                    //restart
                }

                //interuptable reset

                keyHeldTime += Time.deltaTime;
            }
        }
    }
}