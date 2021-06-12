using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GM.Platform;

namespace Frogs.Instances.Cameras {
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] new Camera camera;

        readonly Rect fullscreen = new Rect(0, 0, 1, 1f);
        readonly Rect middle = new Rect(0, .25f, 1, .5f);
        readonly Rect top = new Rect(0, 0.5f, 1, .5f);
        readonly Rect bottom = new Rect(0, 0f, 1, .5f);

        public void Setup(ViewMode viewMode)
        {
            switch (viewMode)
            {
                case ViewMode.Single:
                    switch (GM.platform)
                    {
                        case PC:
                            camera.rect = middle;
                            break;

                        case Android:
                            camera.rect = fullscreen;
                            break;
                    }
                    break;

                case ViewMode.SplitTop:
                    camera.rect = top;
                    ExcludeLayerFromCamera(GM.player2UILayer);
                    break;

                case ViewMode.SplitBottom:
                    camera.rect = bottom;
                    ExcludeLayerFromCamera(GM.player1UILayer);
                    break;
            }
        }

        void ExcludeLayerFromCamera(string layer)
        {
            int mask = ~LayerMask.GetMask(layer);
            camera.cullingMask = mask;
        }
    }
}
