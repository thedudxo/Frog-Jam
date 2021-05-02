using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pursuits;

namespace Tests.Pursuits
{
    public class PursuitTest
    {
        Pursuit pursuit;
        /*
        void LogList()
        {
            foreach (string s in pursuit.LastTickLog)
            {
                Debug.Log(s);
            }
        }

        void SetupPursuit()
        {
            pursuit = new Pursuit();
            pursuit.add.pursuerStartPos = -1;
        }

        
        Pursuer GetNewPursuer()
        {
            return new Pursuer(new MockPositionController());
        }

        Runner AddBasicRunner()
        {
            Runner r = new Runner(new MockPositionController());
            pursuit.add.Runner(r, GetNewPursuer);
            return r;
        }

        [Test]
        public void AddRunner()
        {
            //setup
            SetupPursuit();

            AddBasicRunner();

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
            AddBasicRunner();
            pursuit.Tick();
            LogList();

            //runners before pursuer arrives
            AddBasicRunner(); 
            AddBasicRunner();

            pursuit.Tick(); 
            LogList();
            pursuit.Tick(); 
            LogList();

            //new runner after pursuer arrived
            AddBasicRunner();

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

            Runner r = AddBasicRunner();
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
            AddBasicRunner();
            pursuit.Tick(2);
            LogList();

            //this is the runner to remove
            Runner r = AddBasicRunner();
            pursuit.Tick(2);
            LogList();

            //add a final one behind to confirm its only adjacency that gets removed
            AddBasicRunner();
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

            var speedyPosController = new MockPositionController(3);
            Runner speedyRunner = new Runner(speedyPosController);
            pursuit.add.Runner(speedyRunner, GetNewPursuer);
        }
        */
    }
}
