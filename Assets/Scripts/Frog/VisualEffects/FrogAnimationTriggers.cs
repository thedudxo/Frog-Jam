using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog
{
    public class FrogAnimationTriggers : MonoBehaviour
    {
        [SerializeField] Frog frog;

        public void DeathAnimationFinished()
        {
            frog.VfxManager.ShowFrogVisuals(false);
        }
    }
}
