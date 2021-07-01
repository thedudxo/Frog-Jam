using UnityEngine;

namespace Frogs.Instances.Cameras
{
    public class TargetWeight : ICameraWeight
    {
        public CameraTarget target;
        Vector3 centerOffset;
        const float maxY = -1f;

        public TargetWeight(Transform camTransform, Frog frog)
        {
            target = new CameraTarget(frog.transform);
            centerOffset = (camTransform.position - target.Position);
        }

        public Vector3 Get()
        {
            return new Vector3
            (
                target.Position.x + centerOffset.x,
                Mathf.Min(target.Position.y, maxY) + centerOffset.y,
                target.Position.z + centerOffset.z
            );
        }
    }
}
