using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public class QuitToMenuInput : MonoBehaviour
    {
        [SerializeField] FrogInputs input;
        KeyCode key => input.GetKeybind(Action.Escape);

        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                GM.QuitToMenu();
            }
        }
    }
}