using Movements;
using UnityEngine;
using Utils.Cameras;

namespace Frogs.Instances.Setups
{
    class CameraOffsetSetup
    {
        Vector3 cameraPosition;
        readonly Camera camera;
        readonly float Offset = .15f;

        public CameraOffsetSetup(Camera camera, float Offset = .15f)
        {
            this.camera = camera;
            this.cameraPosition = camera.transform.position;
            this.Offset = Offset;
        }

        public Vector3 PositionWithTargetOffset(Target target)
        {
            float
                z = GetZ(target),
                y = GetY(target),
                x = GetX(z);

            return new Vector3(x, y, z);
        }

        private float GetY(Target target)
        {
            return target.Position.y + 4;
        }

        private float GetZ(Target target)
        {
            return cameraPosition.z - target.Position.z;
        }

        float GetX(float ZPos)
        {
            var unityCamera = new UnityCameraFacade(camera);
            float TargetX = unityCamera.GetWorldPositionOffsetFromScreenLeftEdge(Offset, ZPos);
            float weight = TargetX - cameraPosition.x;
            return weight;
        }
    }
}
