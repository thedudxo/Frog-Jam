using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances.Setups
{
    class CameraSetup : ISetup
    {
        readonly Camera camera;

        public CameraSetup(Frog frog)
        {
            camera = frog.controllers.camera.GetCamera();
        }

        public void Setup(Conditions c)
        {
            camera.SetRectAndRotation(c);
        }

        
    }
}
