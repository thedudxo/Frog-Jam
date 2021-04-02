namespace Chaseable
{
    /// <summary>
    /// Something that can be chased by things IChaserManager manages
    /// </summary>
    public interface IChaseable
    {
        float GetXPos();
        bool CanChase { get; set; }
    }
}
