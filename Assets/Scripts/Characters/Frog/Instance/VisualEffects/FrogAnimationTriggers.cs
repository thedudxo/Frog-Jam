using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frogs
{
    public class FrogAnimationTriggers : MonoBehaviour
    {
        [SerializeField] Frog frog;

        public void DeathAnimationFinished()
        {
            frog.controllers.vfx.ShowFrogVisuals(false);
        }
    }
}
