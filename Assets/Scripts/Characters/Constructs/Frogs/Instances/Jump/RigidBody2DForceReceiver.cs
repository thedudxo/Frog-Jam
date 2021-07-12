using UnityEngine;

namespace Frogs.Instances.Jumps
{
    class RigidBody2DForceReceiver : IForceReceiver
    {
        readonly Rigidbody2D rb;
        public RigidBody2DForceReceiver(Rigidbody2D rb) => this.rb = rb;
        public void AddForce(Vector2 force) => rb.AddForce(force);
        public void AddTorque(float torque) => rb.AddTorque(torque);
    }
}
