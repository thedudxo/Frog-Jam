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
        [SerializeField] Cameras.CameraSetup cameraSetup;

        public void Setup(ViewMode viewMode)
        {
            frog.ViewMode = viewMode;

            switch (viewMode)
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

            cameraSetup.Setup(viewMode);
        }

        void SetupPlayer1()
        {
            Hud.MoveUiToTop();
            frog.UILayer = GM.player1UILayer;
        }

        void SetupPlayer2()
        {
            Hud.MoveUiToBottom();
            frog.controllers.input.SetPlayer2DefaultControlls();
            frog.UILayer = GM.player2UILayer;
        }
    }
}
