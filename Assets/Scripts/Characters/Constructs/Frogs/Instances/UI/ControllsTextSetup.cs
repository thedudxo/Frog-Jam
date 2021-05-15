using System.Collections.Generic;
using UnityEngine;
using Levels.UI;

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
                (prefab, frog.SetObjectUILayer)
                [0];

            controllsTextTip.SetControlls(frog.controllers.input.jump.key, frog.controllers.input.suicide.key);
        }
    }
}