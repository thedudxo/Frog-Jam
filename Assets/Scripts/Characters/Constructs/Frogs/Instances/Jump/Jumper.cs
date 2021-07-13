using UnityEngine;
using System.Collections.Generic;
using Utils;

namespace Frogs.Instances.Jumps
{
    public class Jumper
    {
        public readonly float Torque = -45f;
        readonly Vector2 jumpForce;
        public float ModifiedJump01 { get; private set; }
        List<IJump01Modifier> modifiers;

        IForceReceiver forceReceiver;

        public Jumper(IForceReceiver forceReceiver, List<IJump01Modifier> modifiers, 
            float forceX = 500, float forceY = 600)
        {
            jumpForce = new Vector2(forceX, forceY);

            this.forceReceiver = forceReceiver;
            this.modifiers = modifiers;
        }

        public void Jump(float jump01)
        {
            Normalise.ThrowExceptionIfNotNormal01(jump01);

            ModifiedJump01 = ApplyModifiers(jump01);
            var jumpForce = GetJumpForce(ModifiedJump01);
            PerformJump(jumpForce);
        }

        float ApplyModifiers(float jump01)
        {
            foreach(IJump01Modifier modifier in modifiers)
            {
                jump01 = modifier.Modify(jump01);
            }
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
