using UnityEngine;
using Levels.UI;
using Frogs.Instances.Inputs;

namespace Frogs.Instances.Setups
{
    class PcTutorialTextSetup : TutorialTextSetup
    {
        readonly KeyCode jump;
        readonly KeyCode suicide;

        public PcTutorialTextSetup(Frog frog) : base(frog) 
        {
            var input = frog.controllers.input;
            jump = input.GetKeybind(Action.Jump);
            suicide = input.GetKeybind(Action.Suicide);
        }

        protected override void PlatformSpecificSetup(TutorialText text)
        {
            text.SetKeyboard(jump, suicide);
        }
    }
}