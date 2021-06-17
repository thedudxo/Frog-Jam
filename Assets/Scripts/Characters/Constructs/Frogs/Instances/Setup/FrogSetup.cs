using Frogs.Collections;
using UnityEngine;

namespace Frogs.Instances.Setups
{
    public enum ViewMode { Single, SplitTop, SplitBottom }

    public class FrogSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogHudSetup Hud;
        [SerializeField] ControllsTextSetup ControllsText;
        [SerializeField] CameraSetup cameraSetup;
        [SerializeField] public FrogLayersSetup layers;

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
            layers.Setup(viewMode);
        }

        void SetupPlayer1()
        {
            frog.UILayer = GM.player1UILayer;
        }

        void SetupPlayer2()
        {
            frog.controllers.input.SetPlayer2DefaultControlls();
            frog.UILayer = GM.player2UILayer;
        }
    }
}
