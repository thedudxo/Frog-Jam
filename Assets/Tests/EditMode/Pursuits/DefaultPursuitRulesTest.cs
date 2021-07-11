using NUnit.Framework;
using Pursuits;

namespace Tests.Pursuits
{
    class DefaultPursuitRulesTest
    {
        DefaultPursuitRules rules;
        MemberListMock members;

        Pursuer pursuer;
        Runner runner;

        [SetUp]
        public void SetUp()
        {
            members = new MemberListMock();
            rules = new DefaultPursuitRules(members);

            pursuer = new Pursuer();
            runner = new Runner();
        }

        void AddTwoPursuitMembersInOrder(PursuitMember member1, PursuitMember member2)
        {
            members.list.Add(member1);
            members.list.Add(member2);
        }

        [Test]
        public void Runnner_Gets_PursuerBehind()
        {
            AddTwoPursuitMembersInOrder(pursuer, runner);

            rules.Check();

            Assert.That(runner.pursuerBehind == pursuer);
        }

        [Test] 
        public void Runner_DoesNotGet_PursuerAhead()
        {
            AddTwoPursuitMembersInOrder(runner, pursuer);
            
            rules.Check();

            Assert.That(runner.pursuerBehind == null);
        }

        bool PursuerWasRemoved => members.removed.Contains(pursuer);
        bool PursuerNotRemoved => members.removed.Contains(pursuer) == false;

        [Test]
        public void PursuerRemoved_If_LastInList()
        {
            AddTwoPursuitMembersInOrder(runner, pursuer);

            rules.Check();

            Assert.That(PursuerWasRemoved);
        }

        [Test]
        public void PursuerRemoved_If_NextMemberIsPursuer()
        {
            members.list.Add(pursuer);
            AddTwoPursuitMembersInOrder(new Pursuer(), runner);

            rules.Check();

            Assert.That(PursuerWasRemoved);
        }

        [Test]
        public void PursuerNotRemoved_If_NextMemberIsRunner()
        {
            AddTwoPursuitMembersInOrder(pursuer, runner);

            rules.Check();

            Assert.That(PursuerNotRemoved);
        }

        [Test]
        public void PursuerRemoved_If_Alone()
        {
            members.list.Add(pursuer);

            rules.Check();

            Assert.That(PursuerWasRemoved);
        }
    }
}
