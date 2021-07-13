using UnityEngine;

namespace Frogs.Instances.Jumps
{
    public class Jumper
    {
        public readonly float minJump01 = .15f;
        public readonly float smallJumpThreshhold01 = 0.3f;
        public readonly float Torque = -45f;
        readonly Vector2 jumpForce = new Vector2(500, 600);

        IForceReceiver forceReceiver;

        public Jumper(IForceReceiver forceReceiver)
        {
            this.forceReceiver = forceReceiver;
        }

        public void Jump(float jump01)
        {
            if (jump01 > 1 || jump01 < 0) 
                throw new System.ArgumentOutOfRangeException("jump01", "Must be between 0 and 1 inclusive");

            jump01 = IncreaseSmallJumpAccuracy(jump01);
            var jumpForce = GetJumpForce(jump01);
            PerformJump(jumpForce);
        }

        public float IncreaseSmallJumpAccuracy(float jump01)
        {
            bool minimumJump = jump01 < smallJumpThreshhold01;
            if (minimumJump) jump01 = minJump01;
            return jump01;
        }

        Vector2 GetJumpForce(float jump01)
        {
            return new Vector2(
                jumpForce.x * jump01,
                jumpForce.y * jump01
                );
        }

        void PerformJump(Vector2 force)
        {
            forceReceiver.AddForce(force);
            forceReceiver.AddTorque(Torque);
        }
    }
}
