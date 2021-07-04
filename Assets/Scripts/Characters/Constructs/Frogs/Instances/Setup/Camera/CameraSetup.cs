using Frogs.Instances.Cameras;
using Movements;
using System.Collections.Generic;
using UnityEngine;
using Utils.Cameras;

namespace Frogs.Instances.Setups
{
    class CameraSetup : ISetup
    {
        readonly CameraMechanics cameraMechanics;
        readonly Frog frog;
        readonly Camera camera;
        readonly CameraRectAndRotationSetup RectAndRotationSetup;

        public CameraSetup(Frog frog)
        {
            this.frog = frog;
            cameraMechanics = frog.controllers.cameraMechanics;
            camera = cameraMechanics.camera;
            RectAndRotationSetup = new CameraRectAndRotationSetup(camera);
        }

        public void Setup(Conditions c)
        {
            RectAndRotationSetup.Setup(c);

            Target frogTarget = new Target(frog.transform);
            MovementByWeights movement = NewMovementByWeights(frogTarget);

            cameraMechanics.Setup(movement, frogTarget );
        }

        public MovementByWeights NewMovementByWeights(Target target)
        {
            List<IVector3Weight> weights = new List<IVector3Weight>()
            {
                new ClosestPursuerWeight(camera.transform, frog),
                new TargetWeight(target),
                new ConstantWeight(camera.transform.position - target.Position)
            };

            return new MovementByWeights(camera.transform, weights);
        }
    }

    class CameraPositionFromScreenEdgeSetup : ISetup
    {
        readonly UnityCameraFacade unityCamera;

        public CameraPositionFromScreenEdgeSetup(Camera camera)
        {
            unityCamera = new UnityCameraFacade(camera);
        }

        public void Setup(Conditions conditions)
        {
            float offset = unityCamera.GetWorldOffsetFromScreenEdgeX(.15f);
        }
    }
}
