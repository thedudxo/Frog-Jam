using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{
    public class FrogAnimationTriggers : MonoBehaviour
    {
        [SerializeField] Frog frog;

        public void DeathAnimationFinished()
        {
            frog.vfxManager.ShowFrogVisuals(false);
        }
    }
}
