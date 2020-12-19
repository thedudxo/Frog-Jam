using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationTriggers : MonoBehaviour
{
    [SerializeField] Frog frog;

    public void DeathAnimationFinished()
    {
        frog.showFrogVisuals(false);
    }
}
