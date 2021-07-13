using UnityEngine;

namespace Frogs.Instances.Jumps
{
    public interface IForceReceiver
    {
        void AddForce(Vector2 force);
        void AddTorque(float torque);
    }
}
