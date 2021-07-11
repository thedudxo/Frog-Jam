using NUnit.Framework;
using Pursuits;

namespace Tests.Pursuits
{
    class MockRules : IPursuitRules
    {
        public void Check() { }
    }

    public class PursuitTest : INotifyOnMemberRemoved
    {
        Pursuit pursuit;

        public bool removed = false;
        public void OnMemberRemoved() => removed = true;

        [SetUp]
        public void Setp()
        {
            pursuit = new Pursuit( new MockRules() );
        }

        [Test]
        public void AddRunner()
        {
            Runner runner = pursuit.Add<Runner>();

            Assert.IsNotNull(runner);
        }

        [Test]
        public void AddPursuer()
        {
            Pursuer pursuer = pursuit.Add<Pursuer>();

            Assert.IsNotNull(pursuer);
        }

        [Test]
        public void RemoveRunner()
        {
            var runner = pursuit.Add<Runner>();
            runner.ToNotifyOnMemberRemoved = this;

            pursuit.Remove(runner);
            pursuit.Tick();

            Assert.That(removed);
        }

        [Test]
        public void RemovePursuer()
        {
            var pursuer = pursuit.Add<Pursuer>();
            pursuer.ToNotifyOnMemberRemoved = this;

            pursuit.Remove(pursuer);
            pursuit.Tick();

            Assert.That(removed);
        }

        [Test]
        public void Log()
        {
            pursuit.Add<Runner>();

            pursuit.Tick();

            Assert.IsNotEmpty(pursuit.LastTickLog);
        }

        [Test]
        public void MemberWithHigherPosition_IsIndexedHigher()
        {
            var r = pursuit.Add<Runner>();
            var p = pursuit.Add<Pursuer>();
            r.position = 15;
            p.position = 10;

            pursuit.Tick();

            Assert.That(r.index == 1);
        }
    }
}
