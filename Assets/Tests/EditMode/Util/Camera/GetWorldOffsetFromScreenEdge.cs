using NUnit.Framework;
using System;
using UnityEngine;
using Utils.Cameras;

namespace Tests.Utils
{
    public class GetWorldOffsetFromScreenEdge
    {
        MockScreen screen;

        class MockScreen : IScreen
        {
            public float screenWidth;

            public MockScreen(float screenWidth) => this.screenWidth = screenWidth;
            public float ScreenWidth => screenWidth;
            public Vector3 ScreenToWorld(Vector3 screenPos) => screenPos;
        }

        [SetUp]
        public void Setup()
        {
            screen = new MockScreen(1000);
        }

        [Test]
        public void ReturnsAFloat()
        {
            float output = screen.GetWorldOffsetFromScreenEdgeX(0.15f);
            Assert.IsNotNull(output);
        }

        [Test]
        #region Cases
        [TestCase(-1f)]
        [TestCase(-0.1f)]
        [TestCase(-5f)]
        [TestCase(1.1f)]
        [TestCase(100f)]
        #endregion
        public void OffsetNotConstrainedBetween01ThrowsException(
            float Offset)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => screen.GetWorldOffsetFromScreenEdgeX(Offset));
        }

        [Test]
        [TestCase(1000, .15f, 150)]
        [TestCase(100, .90f, 90)]
        [TestCase(5000, .50f, 2500)]
        public void ReturnsCorrectValue(int screenWidth,
        float offsetPercent, float expectedOffset)
        {
            screen.screenWidth = screenWidth;

            float output = screen.GetWorldOffsetFromScreenEdgeX(offsetPercent); //the method being tested

            Assert.AreEqual(expectedOffset, output);
        }
    }
}
