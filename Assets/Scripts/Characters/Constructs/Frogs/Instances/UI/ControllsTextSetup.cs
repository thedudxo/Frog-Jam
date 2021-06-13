using System.Collections.Generic;
using UnityEngine;
using Levels.UI;
using Frogs.Instances.Inputs;

namespace Frogs.Instances.UI
{
    public class ControllsTextSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        void Start()
        {
            var prefab = new List<GameObject> { frog.currentLevel.controllsTextTipPrefab.gameObject };

            ControllsTextTip controllsTextTip = 
                ObjectInstanceBuilder.CreateInstances<ControllsTextTip>
                (prefab, frog.setup.layers.SetObjectUILayer)
                [0];

            FrogInputs input = frog.controllers.input;

            controllsTextTip.SetControlls(
                input.GetKeybind(Action.Jump),
                input.GetKeybind(Action.Suicide)
                );
        }
    }
}