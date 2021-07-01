﻿using Frogs.Instances.Visuals;
using Frogs.Instances.Jump;
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
        [SerializeField] public new CameraController camera;
        [SerializeField] public JumpController jump;
        [SerializeField] public FrogInputs input;
        [SerializeField] public FrogCleanJumpManager cleanJumpEffects;
        [SerializeField] new public FrogAudio audio;
        [SerializeField] public FrogStateContext stateContext;
    }
}
