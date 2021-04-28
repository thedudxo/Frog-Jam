namespace Pursuits
{

    public class Add
    {
        readonly Pursuit pursuit;

        public float runnerStartPos = 0;
        public float pursuerStartPos = -10;
       
        internal Add(Pursuit pursuit)
        {
            this.pursuit = pursuit;
        }

        public delegate IpostitonController GetPosController();

        public Runner Runner(IpostitonController postitonController, GetPosController getPursuerPositionController)
        {
            Runner runner = CreateNewRunner();
            runner.positionController = postitonController;
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
                    pursuit.add.Pursuer(getPursuerPositionController());
                }
            }
        }

        public Pursuer Pursuer(IpostitonController positionController)
        {
            Pursuer pursuer = new Pursuer();
            pursuer.position = pursuerStartPos;

            pursuit.incomingPursuer = pursuer;

            pursuer.positionController = positionController;

            return pursuer;
        }
    }
}
