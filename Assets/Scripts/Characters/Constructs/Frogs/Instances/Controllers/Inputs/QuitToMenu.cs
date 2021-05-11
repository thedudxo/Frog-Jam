using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public class QuitToMenu : MonoBehaviour
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