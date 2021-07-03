using Movements;
using UnityEngine;

namespace Frogs.Instances.Cameras
{
    public class CameraMechanics : MonoBehaviour
    {
        [SerializeField] public new Camera camera;

        MovementByWeights MovementByWeights;

        public Target Target { get; private set; }

        public void Setup(MovementByWeights movement, Target target)
        {
            this.MovementByWeights = movement;
            this.Target = target;
        }

        private void FixedUpdate()
        {
            MovementByWeights.Move();
        }
    }
}
