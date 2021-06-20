using UnityEngine;

namespace Characters.Instances.Inputs
{
    public interface IKeybindings<Actions> where Actions: System.Enum
    {
        void ChangeKeybind(Actions action, KeyCode key);
        KeyCode GetKeybind(Actions action);
    }
}