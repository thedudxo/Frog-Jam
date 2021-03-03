using System.Collections;
using UnityEngine;
using LevelScripts;

namespace FrogScripts
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