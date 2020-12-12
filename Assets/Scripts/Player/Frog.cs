using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] FrogCamera frogCamera { get; }
    [SerializeField] FrogControlls frogControlls { get; }
    [SerializeField] FrogDeath frogDeath { get; }
    [SerializeField] FrogMetaBloodSplater frogMetaBloodSplater { get; }
    [SerializeField] FrogDynamicEffects frogDynamicEffects { get; }
    [SerializeField] Wave wave { get; }
}
