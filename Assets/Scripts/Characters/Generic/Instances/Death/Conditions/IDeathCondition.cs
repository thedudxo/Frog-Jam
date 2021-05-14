namespace Characters.Instances.Deaths
{
    public interface IDeathCondition
    {
        bool Enabled { get; set; }
        DeathInformation Check();
    }
}
