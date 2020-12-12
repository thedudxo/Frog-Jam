using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] FrogCamera frogCamera;
    [SerializeField] FrogControlls frogControlls;
    [SerializeField] FrogDeath frogDeath;
    [SerializeField] FrogMetaBloodSplater frogMetaBloodSplater;
    [SerializeField] FrogDynamicEffects frogDynamicEffects;
    [SerializeField] Wave wave;

    CameraTarget cameraTarget;


    private void Awake()
    {
        cameraTarget = new CameraTarget(transform);
        frogCamera.target = cameraTarget;
        //cameraTarget.SetTarget(transform);
    }
}
