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

            Target target = new Target(frog.transform);
            MovementByWeights movement = NewMovementByWeights(target);

            cameraMechanics.Setup(movement, target);
        }

        public MovementByWeights NewMovementByWeights(Target target)
        {
            

            List<IVector3Weight> weights = new List<IVector3Weight>()
            {
                new ClosestPursuerWeight(camera.transform, frog),
                new TargetWeight(target),
                new ConstantWeight(OffsetCamera(target))
            };

            return new MovementByWeights(camera.transform, weights);
        }

        private Vector3 OffsetCamera(Target target)
        {
            Vector3 cameraPosition = camera.transform.position;
            float ZPos = cameraPosition.z - target.Position.z;

            float YPos = target.Position.y + 4;

            return new Vector3(GetXPos(), YPos, ZPos);

            float GetXPos()
            {
                var unityCamera = new UnityCameraFacade(camera);
                float TargetX = unityCamera.GetWorldPositionOffsetFromScreenLeftEdge(.15f, ZPos);
                float weight = TargetX - cameraPosition.x;
                return weight;
            }
        }
    }
}
