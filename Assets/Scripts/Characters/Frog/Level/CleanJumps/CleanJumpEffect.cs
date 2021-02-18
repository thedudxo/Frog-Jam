using System.Collections;
using UnityEngine;

namespace FrogScripts
{
    public class CleanJumpEffect : MonoBehaviour
    {
        [SerializeField] DisableOverTime goodJobText;
        [SerializeField] public CleanlyJumpableObstacle obstacle;
        public void SetUp(Frog frog)
        {
            //frog.cleanJumpEffectsManager.AddManaged(this);
        }

        public void DoEffects()
        {
            goodJobText.EnableObject();
        }
    }
}