using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    //unrefactored stuff
    [SerializeField] FrogControlls frogControlls;
    [SerializeField] FrogMetaBloodSplater frogMetaBloodSplater;
    [SerializeField] FrogDynamicEffects frogDynamicEffects;
    [SerializeField] Wave wave;


    // managed classes setups

    [SerializeField] Camera playerCamera;
    CameraController cameraController;
    public Transform cameraTransform { get; private set; }
    CameraTarget cameraTarget;

    FrogLifeState lifeState;

    FrogVfxManager vfxManager;

    //assinged in inspector

    [Header("vfx")]
    [SerializeField] ParticleSystem respawnParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] List<GameObject> visuals;

    [Header("animation")]
    [SerializeField] Animator animator;

    //assigned in awake
    Rigidbody2D rb;
    new Collider2D collider;


    
    public GM.VoidBoolDelg showFrogVisuals;

    private void Initalise()
    {
        //managed classes
        cameraTarget = new CameraTarget(transform);
        cameraController = new CameraController(playerCamera.transform, cameraTarget);
        vfxManager = new FrogVfxManager(animator, transform, respawnParticles, deathParticles, visuals);
        lifeState = new FrogLifeState(transform,wave,rb,collider, vfxManager, cameraTarget,currentCollisions);

        //variables
        cameraTransform = playerCamera.transform;

        //delegates
        showFrogVisuals = vfxManager.ShowFrogVisuals;

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        //get rid of this at some point
        FrogManager.frog = this; 

        Initalise();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        cameraController.FindTargetPos();
        lifeState.Update();
    }

    private void FixedUpdate()
    {
        cameraController.moveTowardsTarget();
    }

    List<GameObject> currentCollisions = new List<GameObject>();
    private void OnTriggerEnter2D   (Collider2D  collision)  { currentCollisions.Add(collision.gameObject); }
    private void OnCollisionEnter2D (Collision2D collision)  { currentCollisions.Add(collision.gameObject); }
    private void OnTriggerExit2D    (Collider2D  collision)  { currentCollisions.Remove(collision.gameObject); }
    private void OnCollisionExit2D  (Collision2D collision)  { currentCollisions.Remove(collision.gameObject); }

    public void ResetFrog() { Initalise(); } //doesnt actually do what level.cs wants (restart the level)
}
