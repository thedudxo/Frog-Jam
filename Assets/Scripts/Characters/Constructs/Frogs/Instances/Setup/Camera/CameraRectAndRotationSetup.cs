using UnityEngine;
using static GM.Platform;

namespace Frogs.Instances.Setups
{
    static class CameraRectAndRotationSetup
    {
        static readonly Rect fullscreen = new Rect(0, 0, 1, 1f);
        static readonly Rect middle = new Rect(0, .25f, 1, .5f);
        static readonly Rect top = new Rect(0, 0.5f, 1, .5f);
        static readonly Rect bottom = new Rect(0, 0f, 1, .5f);

        public static void SetRectAndRotation(this Camera camera, Conditions c)
        {
            switch (c.ViewMode)
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
                    if (GM.platform == Android)
                    {
                        camera.transform.rotation = Quaternion.Euler(0, 0, 180);
                    }
                    ExcludeLayerFromCamera(GM.player2UILayer);
                    break;

                case ViewMode.SplitBottom:
                    camera.rect = bottom;
                    ExcludeLayerFromCamera(GM.player1UILayer);
                    break;
            }

            void ExcludeLayerFromCamera(string layer)
            {
                int mask = ~LayerMask.GetMask(layer);
                camera.cullingMask = mask;
            }
        }
    }
}
