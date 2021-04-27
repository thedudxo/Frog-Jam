using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pursuits;

namespace Tests.Pursuits
{

    public class PursuitTest
    {
        void LogList(List<PursuitMember> members)
        {
            Debug.Log("__________VVV_________");
            foreach(PursuitMember m in members)
            {
                Debug.Log($"{m} at { m.position}");
            }
        }

        [Test]
        public void AddRunner()
        {
            //setup
            var expected = new List<PursuitMember>();

            var movement = new MovementMock();
            movement.movementAmmount = 5;

            Pursuit pursuit = new Pursuit(movement);
            pursuit.add.pursuerStartPos = -1;

            Runner r = pursuit.add.Runner();
            Pursuer p = pursuit.incomingPursuer;
            

            //perform
            pursuit.Tick();

            //assert
            LogList(pursuit.members);
            expected.Add(p);
            expected.Add(r);
            CollectionAssert.AreEqual(expected, pursuit.members);
        }
    }
}
