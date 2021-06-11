using Frogs.Collections;
using Frogs.Instances.UI;
using UnityEngine;

namespace Frogs.Instances
{
    public enum ViewMode { Single, SplitTop, SplitBottom }

    public class FrogSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogHudSetup Hud;
        [SerializeField] ControllsTextSetup ControllsText;

        const string player1UILayer = "Player1 UI";
        const string player2UILayer = "Player2 UI";

        public void Setup(ViewMode veiwMode)
        {
            Debug.Log(FrogInstantiateSettings.veiwMode);
            Debug.Log(GM.platform);

            switch (veiwMode)
            {
                case ViewMode.Single:
                    break;

                case ViewMode.SplitTop:
                    SetupPlayer1();
                    break;

                case ViewMode.SplitBottom:
                    SetupPlayer2();
                    break;
            }
        }

        void SetupPlayer1()
        {
            Hud.MoveUiToTop();
            frog.UILayer = player1UILayer;
            HidePlayerUI(player2UILayer);
        }

        void SetupPlayer2()
        {
            Hud.MoveUiToBottom();
            frog.controllers.input.SetPlayer2DefaultControlls();
            frog.UILayer = player2UILayer;
            HidePlayerUI(player1UILayer);
        }

        void HidePlayerUI(string layer)
        {
            int mask = ~LayerMask.GetMask(layer);
            frog.controllers.camera.SetLayerMask(mask);
        }
    }
}
