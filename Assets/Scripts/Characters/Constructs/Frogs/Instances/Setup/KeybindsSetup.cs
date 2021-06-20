using Frogs.Instances.Inputs;
using UnityEngine;

namespace Frogs.Instances.Setups
{
    class KeybindsSetup : ISetup
    {
        Characters.Instances.Inputs.IKeybindings<Action> keybindings;

        public KeybindsSetup(Frog frog) {
            keybindings = frog.controllers.input;
        }

        public void Setup(Conditions c)
        {
            bool Player2OnPC = c.Platform == GM.Platform.PC && c.ViewMode == ViewMode.SplitBottom;

            if (Player2OnPC)
            {
                SetPlayer2DefaultKeybinds();
            }
        }

        void SetPlayer2DefaultKeybinds()
        {
            keybindings.ChangeKeybind(Action.Jump, KeyCode.UpArrow);
            keybindings.ChangeKeybind(Action.Suicide, KeyCode.RightShift);
        }
    }
}
