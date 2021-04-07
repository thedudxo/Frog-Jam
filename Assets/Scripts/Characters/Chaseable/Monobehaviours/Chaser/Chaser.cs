using UnityEngine;

namespace Chaseables.MonoBehaviours
{
    public abstract class Chaser : MonoBehaviour, IChaser
    {
        public abstract void CheckStopChaseConditions();

        public abstract float GetSpeed();

        public virtual float GetXPos() => transform.position.x;

        public abstract bool IsBehind(IChaseable chaseable);
    }
}
