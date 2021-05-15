using System.Collections;
using UnityEngine;
using Levels;

namespace Frogs
{
    public class CleanJumpEffect : MonoBehaviour
    {
        [SerializeField] DisableOverTime goodJobText;
        [SerializeField] public CleanlyJumpableObstacle obstacle;

        public void DoEffects()
        {
            goodJobText.EnableObject();
        }
    }
}