using UnityEngine;

namespace Movements
{
    public class TargetWeight : IVector3Weight
    {
        public Target target;
        const float maxY = -1f;

        public TargetWeight(Target target)
        {
            this.target = target;
        }

        public Vector3 Get()
        {
            return new Vector3
            (
                target.Position.x,
                Mathf.Min(target.Position.y, maxY),
                target.Position.z
            );
        }
    }


    public class ConstantWeight : IVector3Weight
    {
        public Vector3 Weight { get; set; }

        public ConstantWeight(Vector3 weight)
        {
            Weight = weight;
        }

        public Vector3 Get()
        {
            return Weight;
        }
    }
}
