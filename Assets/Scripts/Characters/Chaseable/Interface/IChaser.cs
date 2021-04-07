namespace Chaseables
{
    public interface IChaser
    {
        bool IsBehind(IChaseable chaseable);

        void CheckStopChaseConditions();

        float GetXPos();

        float GetSpeed();
    }
}
