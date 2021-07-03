using UnityEngine;

namespace Movements
{
    public class TargetWeight : IVector3Weight
    {
        public Target target;
        Vector3 centerOffset;
        const float maxY = -1f;

        public TargetWeight(Transform camTransform, Target target)
        {
            this.target = target;
            centerOffset = camTransform.position - target.Position;
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
