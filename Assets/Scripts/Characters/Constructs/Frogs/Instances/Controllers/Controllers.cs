using Frogs.Instances.Visuals;
using Frogs.Instances.Jumps;
using Frogs.Instances.Inputs;
using Frogs.Instances.State;
using Frogs.Instances.Audio;
using UnityEngine;
using Frogs.Instances.Cameras;

namespace Frogs.Instances
{
    public class Controllers : MonoBehaviour
    {
        [SerializeField] public VfxController vfx;
        [SerializeField] public CameraMechanics cameraMechanics;
        [SerializeField] public FrogInputs input;
        [SerializeField] public JumpController jump;
        [SerializeField] public FrogCleanJumpManager cleanJumpEffects;
        [SerializeField] new public FrogAudio audio;
        [SerializeField] public FrogStateContext stateContext;
    }
}
