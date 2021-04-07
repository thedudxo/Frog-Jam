using System.Collections.Generic;

namespace Chaseables
{
    public interface IChaseable
    {
        float GetXPos();
        bool IsCurrentlyChaseable { get; set; }

        IChaserCollection ChaserCollection { get; }
        IChaser ActiveChaser { get; set; }

        bool WillSetbackBehindAChaser(float setbackDistance, float respawnTimeSeconds);

    }
}
