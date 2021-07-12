using UnityEngine;

namespace Frogs.Instances.Jumps
{
    interface IForceReceiver
    {
        void AddForce(Vector2 force);
        void AddTorque(float torque);
    }
}
