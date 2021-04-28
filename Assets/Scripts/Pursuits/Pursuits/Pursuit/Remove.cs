namespace Pursuits
{
    public class Remove
    {
        Pursuit pursuit;
        internal Remove(Pursuit pursuit)
        {
            this.pursuit = pursuit;
        }

        public void Runner(Runner runner)
        {
            pursuit.members.Remove(runner);
        }

        public void Pursuer(Pursuer pursuer)
        {
            pursuit.members.Remove(pursuer);
        }
    }
}
