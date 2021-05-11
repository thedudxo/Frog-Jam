namespace Characters.Instances.Deaths
{
    public interface IRespawnMethod
    {
        int Priority { get; }
        void Respawn();
    }
}
