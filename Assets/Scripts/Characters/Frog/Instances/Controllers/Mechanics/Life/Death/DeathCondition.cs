namespace Frogs.Instances.Death
{
    public abstract class DeathCondition
    {
        Frog frog;

        internal DeathCondition(Frog frog)
        {
            this.frog = frog;
        }

        internal abstract DeathType CheckCondition();
    }
}
