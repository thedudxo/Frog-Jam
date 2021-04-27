//using LevelScripts;

namespace Pursuits
{
    public interface IMemberMovement
    {
        Pursuit pursuit { get; set; }
        void Move();
    }
}
