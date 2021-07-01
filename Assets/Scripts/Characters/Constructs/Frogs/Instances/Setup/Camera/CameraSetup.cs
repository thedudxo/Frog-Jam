using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Cameras;

namespace Frogs.Instances.Setups
{
    class CameraSetup : ISetup
    {
        readonly Camera camera;
        readonly CameraRectAndRotationSetup RectAndRotationSetup;

        public CameraSetup(Frog frog)
        {
            camera = frog.controllers.camera.GetCamera();
            RectAndRotationSetup = new CameraRectAndRotationSetup(camera);
        }

        public void Setup(Conditions c)
        {
            RectAndRotationSetup.Setup(c);
        }
    }

    class CameraOffsetSetup : ISetup
    {
        readonly UnityCameraFacade unityCamera;

        public CameraOffsetSetup(Camera camera)
        {
            unityCamera = new UnityCameraFacade(camera);
        }

        public void Setup(Conditions conditions)
        {
            var offset = unityCamera.GetWorldOffsetFromScreenEdgeX(.15f);

        }
    }
}
