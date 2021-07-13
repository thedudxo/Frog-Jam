using NUnit.Framework;
using Frogs.Instances.Jumps;
using UnityEngine;
using System;

namespace Tests.Jumps
{
    class MockForceReceiver : IForceReceiver
    {
        public Vector2 force;
        public float torque;
        public void AddForce(Vector2 force) => this.force += force;
        public void AddTorque(float torque) => this.torque += torque;
    }

    class JumperTests
    {
        Jumper jumper;
        MockForceReceiver forceReceiver;

        [SetUp]
        public void SetUp()
        {
            forceReceiver = new MockForceReceiver();
            jumper = new Jumper(forceReceiver);
        }

        [Test]
        public void JumpAddsForce()
        {
            jumper.Jump(0.5f);

            bool someForceWasAdded = forceReceiver.force != Vector2.zero;
            Assert.That(someForceWasAdded);
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
        public void IncreaseSmallJumpAccuracy_WhenBelowThreshold()
        {
            float smallJump = jumper.smallJumpThreshhold01 - 0.1f;

            float result = jumper.IncreaseSmallJumpAccuracy(smallJump);

            Assert.That(result == jumper.minJump01);
        }
    }
}
