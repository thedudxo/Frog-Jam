using UnityEngine;

namespace Frogs.Instances.Jumps
{
    class Jumper
    {
        const float minJumpCharge01 = .15f;
        const float smallJumpThreshhold01 = 0.3f;
        readonly Vector2 jumpForce = new Vector2(500, 600);

        IForceReceiver forceReceiver;

        public Jumper(IForceReceiver forceReceiver)
        {
            this.forceReceiver = forceReceiver;
        }

        public void Jump(float jump01)
        {
            jump01 = IncreaseSmallJumpAccuracy(jump01);
            var jumpForce = GetJumpForce(jump01);
            PerformJump(jumpForce);
        }

        float IncreaseSmallJumpAccuracy(float jump01)
        {
            bool minimumJump = jump01 < smallJumpThreshhold01;
            if (minimumJump) jump01 = minJumpCharge01;
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
            forceReceiver.AddTorque(-45);
        }
    }
}
