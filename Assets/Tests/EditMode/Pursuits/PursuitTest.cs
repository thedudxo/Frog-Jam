using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pursuits;

namespace Tests.Pursuits
{
    public class PursuitTest
    {
        Pursuit pursuit;

        void LogList()
        {
            foreach (string s in pursuit.LastTickLog)
            {
                Debug.Log(s);
            }
        }

        void SetupPursuit()
        {
            var MockPosAssigner = new MockPositionControllerAssigner();
            pursuit = new Pursuit(MockPosAssigner, MockPosAssigner);
            pursuit.add.pursuerStartPos = -1;
        }

        [Test]
        public void AddRunner()
        {
            //setup
            SetupPursuit();

            pursuit.add.Runner();

            //perform
            pursuit.Tick();
            LogList();

            //assert
            Assert.That(pursuit.incomingPursuer != null);
            Assert.That(pursuit.members[0] is Runner);
        }

        [Test] 
        public void AddSeveralRunners()
        { 
            SetupPursuit();

            //runner that creates the pursuer
            pursuit.add.Runner();
            pursuit.Tick();
            LogList();

            //runners before pursuer arrives
            pursuit.add.Runner();
            pursuit.add.Runner();

            pursuit.Tick(); 
            LogList();
            pursuit.Tick(); 
            LogList();

            //new runner after pursuer arrived
            pursuit.add.Runner();

            pursuit.Tick();
            LogList();
            pursuit.Tick();
            LogList();

            Assert.That(pursuit.members[0] is Pursuer);
            Assert.That(pursuit.members[1] is Runner);
            Assert.That(pursuit.members[2] is Pursuer);
            Assert.That(pursuit.members[3] is Runner);
            Assert.That(pursuit.members[4] is Runner);
            Assert.That(pursuit.members[5] is Runner);
        }

        [Test]
        public void RemoveRunner()
        {
            SetupPursuit();

            Runner r = pursuit.add.Runner();
            pursuit.Tick(2);
            LogList();

            pursuit.remove.Runner(r);
            pursuit.Tick();
            LogList();

            CollectionAssert.IsEmpty(pursuit.members);
        }

        [Test]
        public void RemoveAdjacentPursuers()
        {
            SetupPursuit();

            //runner ahead to create an adjacent pursuer
            pursuit.add.Runner();
            pursuit.Tick(2);
            LogList();

            //this is the runner to remove
            Runner r = pursuit.add.Runner();
            pursuit.Tick(2);
            LogList();

            //add a final one behind to confirm its only adjacency that gets removed
            pursuit.add.Runner();
            pursuit.Tick(2);
            LogList();

            //remove that runner
            pursuit.remove.Runner(r);
            pursuit.Tick();
            LogList();

            //assert
            Assert.That(pursuit.members[0] is Pursuer);
            Assert.That(pursuit.members[1] is Runner);
            Assert.That(pursuit.members[2] is Pursuer);
            Assert.That(pursuit.members[3] is Runner);
        }

        [Test]
        public void SpeedyRunner()
        {
            SetupPursuit();

            pursuit.add.Runner();
            MockPositionController positionController = pursuit.members[0].positionController as MockPositionController;
        }
    }
}
