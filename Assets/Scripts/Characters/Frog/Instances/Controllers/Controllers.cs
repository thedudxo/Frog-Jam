using Frogs.Life;
using Frogs.Vfx;
using Frogs.Jump;
using Frogs.Instances.Inputs;
using UnityEngine;

namespace Frogs
{
    public class Controllers : MonoBehaviour
    {
        [SerializeField] public VfxController vfx;
        [SerializeField] public LifeController life;
        [SerializeField] public new CameraController camera;
        [SerializeField] public JumpController jump;
        [SerializeField] public FrogInputs input;
        [SerializeField] public FrogCleanJumpManager cleanJumpEffects;
    }
}
