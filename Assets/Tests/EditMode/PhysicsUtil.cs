using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static PhysicsUtil1D;

namespace Tests
{
    public class PhysicsUtil
    {
        [Test]
        public void TestDirectionToPos()
        {
            Assert.That(DirectionToPosition(10f, 9f) == -1);
            Assert.That(DirectionToPosition(1f, 50f) == 1);
            Assert.That(DirectionToPosition(-45.2f, 45.2f) == 1);
            Assert.That(DirectionToPosition(0.1f, 0.09f) == -1);
        }

        [Test]
        public void TestTimeToStop()
        {
            Assert.That(TimeToStop(10,10) == 1);
            Assert.That(TimeToStop(-10, 10) == 1);
            Assert.That(TimeToStop(10, -10) == 1);
            Assert.That(TimeToStop(-10, -10) == 1);
        }

        [Test]
        public void TestTimeToPos()
        {
            Assert.That(TimeToPos(10, 10) == 1);
            Assert.That(TimeToPos(20, 10) == 2);
            Assert.That(TimeToPos(10, 20) == 0.5f);

            Assert.That(TimeToPos(-10, 20) == float.PositiveInfinity);
            Assert.That(TimeToPos(10, -20) == float.PositiveInfinity);

            Assert.That(TimeToPos(-10, -10) == 1);
            Assert.That(TimeToPos(-10, -20) == 0.5f);
        }

        [Test]
        public void TestWillOvershoot()
        {
            Assert.That(WillOvershoot(10, 0, -1, 10) == false);
            Assert.That(WillOvershoot(10, 0, -11, 1) == true);
        }

        [Test]
        public void TestDirection()
        {
            Assert.That(Direction(8246) == 1);
            Assert.That(Direction(-2349) == -1);
        }
    }
}
