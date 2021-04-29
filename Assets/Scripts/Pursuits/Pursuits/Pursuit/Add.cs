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

        public delegate void CreateNewPursuer();

        public void Runner(CreateNewPursuer CreateNewPursuer)
        {
            Runner runner = new Runner();
            runner.position = runnerStartPos;

            pursuit.members.Insert(0, runner);

            if (pursuit.incomingPursuer == null)
            {
               CreateNewPursuer();
            }
        }

        public Pursuer Pursuer()
        {
            Pursuer pursuer = new Pursuer();
            pursuer.position = pursuerStartPos;

            pursuit.incomingPursuer = pursuer;
            return pursuer;
        }
    }
}
