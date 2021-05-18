using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public class QuitToMenuInput : MonoBehaviour
    {
        [HideInInspector] public KeyCode key = KeyCode.Escape;

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                GM.QuitToMenu();
            }
        }
    }
}