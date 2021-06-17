using System.Collections.Generic;
using UnityEngine;
using Levels.UI;
using Frogs.Instances.Inputs;
using static GM.Platform;

namespace Frogs.Instances.Setups
{
    public class ControllsTextSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        void Start()
        {
            //objectInstanceBuilder handles lists, but we only want one thing
            var prefab = new List<GameObject> {
                frog.currentLevel.controllsTextTipPrefab.gameObject
            };

            ControllsTextTip controllsTextTip =
                ObjectInstanceBuilder.CreateInstances<ControllsTextTip>
                (prefab, frog.setup.layers.SetObjectUILayer)
                [0];

            switch (GM.platform) 
            {
                case PC:
                    FrogInputs input = frog.controllers.input;

                    controllsTextTip.SetKeyboard(
                        input.GetKeybind(Action.Jump),
                        input.GetKeybind(Action.Suicide));
                    break;

                case Android:
                    controllsTextTip.SetMobile();
                    break;
            }
        }
    }
}