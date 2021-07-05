using UnityEngine;
using static GM.Platform;

namespace Frogs.Instances.Setups
{
    class CameraRotationSetup :ISetup
    {
        Camera camera;

        public CameraRotationSetup(Camera camera)
        {
            this.camera = camera;
        }

        public void Setup(Conditions conditions)
        {
            SetCameraRotation(conditions);
        }

        void SetCameraRotation(Conditions c)
        {
            if (c.ViewMode == ViewMode.SplitTop)
            {
                if (GM.platform == Android)
                {
                    camera.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
    }
}
