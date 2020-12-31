using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog
{
    public class FrogAnimationTriggers : MonoBehaviour
    {
        [SerializeField] FrogController frog;

        public void DeathAnimationFinished()
        {
            frog.vfxManager.ShowFrogVisuals(false);
        }
    }
}
