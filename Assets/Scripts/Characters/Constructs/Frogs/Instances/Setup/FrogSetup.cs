using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs.Instances.UI;
using Frogs.Collections;

namespace Frogs.Instances
{
    public enum ViewMode { Single, SplitTop, SplitBottom }

    public class FrogSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogHudSetup Hud;
        [SerializeField] ControllsTextSetup ControllsText;

        public void Setup(ViewMode veiwMode)
        {
            Debug.Log(FrogInstantiateSettings.veiwMode);
            Debug.Log(GM.platform);

            switch (veiwMode)
            {
                case ViewMode.Single:
                    break;

                case ViewMode.SplitTop:
                    Hud.MoveUiToTop();
                    break;

                case ViewMode.SplitBottom:
                    Hud.MoveUiToBottom();
                    frog.controllers.input.SetPlayer2DefaultControlls();
                    break;
            }
        }
    }
}
