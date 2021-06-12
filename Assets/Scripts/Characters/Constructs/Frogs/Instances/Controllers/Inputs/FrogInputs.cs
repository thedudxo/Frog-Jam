using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public enum Action { Jump, Suicide, Escape }

    public class FrogInputs : MonoBehaviour
    {
        [SerializeField] public FrogSuicideInput suicide;
        [SerializeField] public JumpInput jump;
        [SerializeField] public QuitToMenuInput quitToMenu;
        [SerializeField] public FrogLevelRestartInput levelRestart;

        Dictionary<Action, KeyCode> keybinds = new Dictionary<Action, KeyCode>
        {
            {Action.Jump, KeyCode.Space},
            {Action.Suicide, KeyCode.Q},
            {Action.Escape, KeyCode.Escape}
        };

        public void ChangeKeybind(Action action, KeyCode key)
        {
            keybinds[action] = key;
            Debug.Log(keybinds[action]);
        }

        public KeyCode GetKeybind(Action action)
        {
            return keybinds[action];
        }

        public void SetPlayer2DefaultControlls()
        {
            ChangeKeybind(Action.Jump, KeyCode.UpArrow);
            ChangeKeybind(Action.Suicide, KeyCode.RightShift);
        }
    }

}