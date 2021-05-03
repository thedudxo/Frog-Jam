using Frogs.Life;
using Frogs.Vfx;
using Frogs.Jump;
using System.Collections.Generic;
using UnityEngine;
using Levels;
using static Frogs.FrogState;

namespace Frogs
{
    public class Controllers : MonoBehaviour
    {
        [SerializeField] public VfxController vfx;
        [SerializeField] public LifeController life;
        [SerializeField] public new CameraController camera;
        [SerializeField] public JumpController jump;
        [SerializeField] public Controlls controlls; //should perhaps be in a settings object instead
        [SerializeField] public FrogCleanJumpManager cleanJumpEffects;
    }
}
