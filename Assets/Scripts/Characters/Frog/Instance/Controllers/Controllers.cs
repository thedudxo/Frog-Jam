using FrogScripts.Life;
using FrogScripts.Vfx;
using FrogScripts.Jump;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;
using static FrogScripts.FrogState;

namespace FrogScripts
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
