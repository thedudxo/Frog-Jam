using Frogs.Instances.Inputs;
using Levels.UI;
using System.Collections.Generic;
using UnityEngine;
using static GM.Platform;
using static ObjectInstanceBuilder;

namespace Frogs.Instances.Setups
{
    class TutorialTextSetup : ISetup
    {
        readonly Frog frog;
        readonly ExtndGameObjSetup SetPlayerUILayer;
        readonly FrogInputs input;

        public TutorialTextSetup(Frog frog)
        {
            this.frog = frog;
            input = frog.controllers.input;
            SetPlayerUILayer = frog.setup.layers.SetObjectUILayer;
        }

        public void Setup(Conditions c)
        {
            TutorialText tutorialText = GetTutorialText();

            switch (c.Platform)
            {
                case PC:
                    bool player2 = c.ViewMode == ViewMode.SplitBottom;
                    if (player2) 
                    {
                        SetPlayer2(tutorialText);
                    }
                    break;

                case Android:
                    tutorialText.SetMobile();
                    break;
            }
        }

        void SetPlayer2(TutorialText tutorialText)
        {
            KeyCode jump = input.GetKeybind(Action.Jump);
            KeyCode suicide = input.GetKeybind(Action.Suicide);
            tutorialText.SetKeyboard(jump, suicide);
        }

        TutorialText GetTutorialText()
        {
            //objectInstanceBuilder handles lists, but we only want one thing
            var level = frog.currentLevel;

            GameObject tutorialPrefab = level.controllsTextTipPrefab.gameObject;
            var prefab = new List<GameObject> { tutorialPrefab };

            TutorialText instancedTutorialText = CreateInstances<TutorialText>
                (prefab, SetPlayerUILayer) [0];

            return instancedTutorialText;
        }
    }
}