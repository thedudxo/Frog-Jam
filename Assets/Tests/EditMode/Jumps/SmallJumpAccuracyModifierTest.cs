using NUnit.Framework;
using Frogs.Instances.Jumps;
using System;

namespace Tests.Jumps
{
    class SmallJumpAccuracyModifierTest
    {
        IncreaseSmallJumpAccuracy modifier;

        [SetUp]
        public void Setup()
        {
            modifier = new IncreaseSmallJumpAccuracy(.1f, .5f);
        }

        [Test]
        public void IncreaseSmallJumpAccuracy_WhenBelowThreshold()
        {
            float smallJump = 0.4f;

            float result = modifier.Modify(smallJump);

            Assert.That(result == .1f);
        }

        [Test]
        [TestCase(-.1f)]
        [TestCase(1.1f)]
        public void Input_OutOf01Range_ThrowsException(float value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => modifier.Modify(value));
        }

        [Test]
        [TestCase(0.5f, 1.1f)]
        [TestCase(-20.1f, 0.1f)]
        public void ConstructorValues_OutOf01Range_ThrowsException(float val1, float val2)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new IncreaseSmallJumpAccuracy(val1, val2));
        }
    }
}
