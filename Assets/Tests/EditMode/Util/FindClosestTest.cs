using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Tests.EditMode.Util
{
    class DummyMonoBehaviour : MonoBehaviour
    {
    }

    public class FindClosestTest
    {

        [Test]
        public void FindCloseTest()
        {
            List<DummyMonoBehaviour> objects = new List<DummyMonoBehaviour>();

            float Pos = -20;
            float increment = 10;

            //populate with spaced out objects
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = new GameObject();
                objects.Add(obj.AddComponent<DummyMonoBehaviour>());
                obj.transform.position = new Vector2(Pos, 0);
                Pos += increment;
            }

            float testPos = 5;

            Assert.That(FindClosest.Behind(objects, testPos) == objects[2] );
            Assert.That(FindClosest.Ahead(objects, testPos).transform.position.x == 10);

            Assert.That(FindClosest.Ahead(new List<DummyMonoBehaviour>(), testPos) == null);
            Assert.That(FindClosest.Behind(objects, -30) == null);
        }
    }
}