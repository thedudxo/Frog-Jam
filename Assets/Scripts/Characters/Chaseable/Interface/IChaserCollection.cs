namespace Chaseables
{
    public interface IChaserCollection
    {
        IChaseableCollection Chasing { get; set; }

        IChaser Chase(IChaseable chaseable);

        void ChaseableStartedLevel(IChaseable chaseable);

        IChaser GetFirstBehindOrNew(float pos);
        IChaser GetFirstBehindOrNull(float pos);
    }
}
