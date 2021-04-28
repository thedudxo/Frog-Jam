namespace Pursuits
{

    public class Add
    {
        readonly Pursuit pursuit;

        public float runnerStartPos = 0;
        public float pursuerStartPos = -10;
        
        //PositionControllerAssigners are used rather than method arguments, because when a runner is created its possible for a pursuer to also be created in the background, which would require being given a new  PositionController instance every time that isn't always used.
        public IPositionControllerAssigner pursuerPosAssigner;
        public IPositionControllerAssigner runnerPosAssigner;

        internal Add(Pursuit pursuit, IPositionControllerAssigner pursuerPosAssigner, IPositionControllerAssigner runnerPosAssigner)
        {
            this.pursuit = pursuit;
            this.pursuerPosAssigner = pursuerPosAssigner;
            this.runnerPosAssigner = runnerPosAssigner;
        }

        void AssignPositionController(IPositionControllerAssigner assigner, PursuitMember member)
        {
            assigner.AssignPositionControllerTo(member);
            if (member.positionController == null)
            {
                throw new System.ArgumentNullException($"New {member} was not assigned An instance of {typeof(IpostitonController)} by {runnerPosAssigner}");
            }
        }

        public Runner Runner()
        {
            Runner runner = CreateNewRunner();
            AssignPositionController(runnerPosAssigner, runner);
            CheckForIncomingPursuer();
            return runner;

            Runner CreateNewRunner()
            {
                Runner r = new Runner();

                r.position = runnerStartPos;

                pursuit.members.Insert(0, r);
                return r;
            }


            void CheckForIncomingPursuer()
            {
                if (pursuit.incomingPursuer == null)
                {
                    pursuit.add.Pursuer();
                }
            }
        }

        public Pursuer Pursuer()
        {
            Pursuer pursuer = new Pursuer();
            pursuer.position = pursuerStartPos;

            pursuit.incomingPursuer = pursuer;

            AssignPositionController(pursuerPosAssigner, pursuer);

            return pursuer;
        }
    }
}
