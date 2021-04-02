using System.Collections;
using UnityEngine;

namespace Chaseable.MonoBehaviours
{
    public class Chaseable : MonoBehaviour, IChaseable
    {
        public virtual bool CanChase { get; set ; }

        public virtual float GetXPos() => transform.position.x;
    }
}