using Frogs.Instances.Cameras;
using Movements;
using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances.Setups
{
    class CameraSetup : ISetup
    {
        readonly CameraMechanics cameraMechanics;
        readonly Frog frog;
        readonly Camera camera;
        readonly CameraRectAndRotationSetup RectAndRotationSetup;
        readonly CameraOffsetSetup cameraOffsetSetup;

        public CameraSetup(Frog frog)
        {
            this.frog = frog;
            cameraMechanics = frog.controllers.cameraMechanics;
            camera = cameraMechanics.camera;

            RectAndRotationSetup = new CameraRectAndRotationSetup(camera);
            cameraOffsetSetup = new CameraOffsetSetup(camera);
        }

        public void Setup(Conditions c)
        {
            Target target = new Target(frog.transform);
            MovementByWeights movement = NewMovementByWeights(target);

            RectAndRotationSetup.Setup(c); //must be done after setting up the weights or TopScreen on mobile will have issues

            cameraMechanics.Setup(movement, target);
        }

        public MovementByWeights NewMovementByWeights(Target target)
        {
            List<IVector3Weight> weights = new List<IVector3Weight>()
            {
                new ClosestPursuerWeight(camera.transform, frog),
                new TargetWeight(target),
                new ConstantWeight(cameraOffsetSetup.PositionWithTargetOffset(target))
            };

            return new MovementByWeights(camera.transform, weights);
        }
    }
}
