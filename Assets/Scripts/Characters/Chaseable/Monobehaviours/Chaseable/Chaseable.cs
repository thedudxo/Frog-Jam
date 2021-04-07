using System.Collections;
using UnityEngine;

namespace Chaseables.MonoBehaviours
{
    public abstract class Chaseable : MonoBehaviour, IChaseable
    {
        public abstract bool IsCurrentlyChaseable { get; set ; }

        public virtual float GetXPos() => transform.position.x;

        public abstract IChaserCollection ChaserCollection { get; }
        public IChaser ActiveChaser { get; set; }


        protected void AttachClosestChaser()
        {
            if (ActiveChaser != null) EndChase();

            ActiveChaser = ChaserCollection.GetFirstBehindOrNew(GetXPos());
        }

        protected void CheckBehind()
        {
            if (ActiveChaser?.IsBehind(this) == false)
                AttachClosestChaser();
        }

        protected virtual void EndChase()
        {
            ActiveChaser?.CheckStopChaseConditions();
            ActiveChaser = null;
        }

        public virtual bool WillSetbackBehindAChaser(float setbackDistance, float respawnTimeSeconds)
        {
            float XPos = GetXPos();

            IChaser chaser = ChaserCollection.GetFirstBehindOrNull(XPos);
            if (chaser == null) return false;

            float chaserPosAtRespawn = chaser.GetXPos() + (chaser.GetSpeed() * respawnTimeSeconds);

            float setbackPos = XPos - setbackDistance;

            bool SetbackBehind = setbackPos < (chaserPosAtRespawn + 0.05);

            if (SetbackBehind) return true;
            else return false;
        }
    }
}