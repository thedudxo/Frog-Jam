using NUnit.Framework;
using Frogs.Instances.Jumps;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Tests.Jumps
{
    class MockForceReceiver : IForceReceiver
    {
        public Vector2 force;
        public float torque;

        public bool AddForceWasCalled = false;
        public void AddForce(Vector2 force) 
        { 
            this.force += force;
            AddForceWasCalled = true;
        }
        public void AddTorque(float torque) => this.torque += torque;
    }

    class MockModifier : IJump01Modifier
    {
        public bool active;

        public float Modify(float jump01)
        {
            if (active)
                return 1f;
            return jump01;
        }
    }

    class JumperTests
    {
        private const float ForceX = 2;
        private const float ForceY = 4;
        Jumper jumper;
        MockForceReceiver forceReceiver;
        List<IJump01Modifier> modifiers;
        MockModifier modifierThatReturns1;

        [SetUp]
        public void SetUp()
        {
            forceReceiver = new MockForceReceiver();
            modifierThatReturns1 = new MockModifier();

            modifiers = new List<IJump01Modifier>()
            {
                modifierThatReturns1
            };

            jumper = new Jumper(forceReceiver, modifiers, ForceX, ForceY);
        }

        [Test]
        public void JumpAddsForce()
        {
            jumper.Jump(0.5f);

            Assert.That(forceReceiver.AddForceWasCalled);
        }

        [Test]
        public void JumpAddsTorque()
        {
            jumper.Jump(0.8f);

            bool someTorqueWasAdded = forceReceiver.torque != 0;

            Assert.That(someTorqueWasAdded);
        }

        [Test]
        [TestCase(1.5f)]
        [TestCase(-1f)]
        [TestCase(-0.01f)]
        [TestCase(1.001f)]
        public void JumpOutOf01Range_ThrowsException(float value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => jumper.Jump(value));
        }

        [Test]
        public void ModfiersGetApplied()
        {
            modifierThatReturns1.active = true;

            jumper.Jump(0.1f);

            Assert.AreEqual(1f, jumper.ModifiedJump01);
        }


        [Test]
        public void InputIsMultipliedByForce()
        {
            jumper.Jump(0.5f);

            Assert.AreEqual(new Vector2(ForceX,ForceY) * 0.5f, forceReceiver.force);
        }
    }
}
