using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Frog.Life;

namespace Frog
{
    public class Frog : MonoBehaviour
    {
        //unrefactored stuff
        [SerializeField] FrogControlls frogControlls;
        [SerializeField] FrogBloodSplater frogMetaBloodSplater;
        [SerializeField] FrogDynamicEffects frogDynamicEffects;
        [SerializeField] public Wave wave;


        // managed classes setups

        [SerializeField] Camera playerCamera;
        [HideInInspector] CameraTracker cameraController;
        [HideInInspector] public Transform CameraTransform { get; private set; }
        [HideInInspector] public CameraTarget CameraTarget { get; private set; }

        [HideInInspector] LifeController lifeController;
        [HideInInspector] public VfxManager VfxManager { get; private set; }

        //assinged in inspector

        [Header("vfx")]
        [SerializeField] public ParticleSystem respawnParticles;
        [SerializeField] public ParticleSystem deathParticles;
        [SerializeField] public List<GameObject> visuals;
        [SerializeField] public Image bloodSplatter;

        [Header("animation")]
        [SerializeField] public Animator animator;

        [Header("audio")]
        [SerializeField] public AudioClip deathSounds;
        [SerializeField] public AudioClip respawnSounds;
        [SerializeField] public AudioClip jumpSounds;
        [SerializeField] public AudioClip landSounds;

        //assigned in awake
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] new public Collider2D collider;

        private void Initalise()
        {
            CameraTarget = new CameraTarget(transform);
            cameraController = new CameraTracker(this);
            VfxManager = new VfxManager(this);
            lifeController = new LifeController(this);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            CameraTransform = playerCamera.transform;

            //get rid of this at some point
            FrogManager.frog = this;

            Initalise();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            cameraController.Update();
            lifeController.Update();
            VfxManager.Update();
        }

        private void FixedUpdate()
        {
            cameraController.MoveTowardsTarget();
        }

        [HideInInspector] public List<GameObject> currentCollisions = new List<GameObject>();
        private void OnTriggerEnter2D(Collider2D collision) { currentCollisions.Add(collision.gameObject); }
        private void OnCollisionEnter2D(Collision2D collision) { currentCollisions.Add(collision.gameObject); }
        private void OnTriggerExit2D(Collider2D collision) { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionExit2D(Collision2D collision) { currentCollisions.Remove(collision.gameObject); }

        public void RestartLevel() { lifeController.Restart(); }
    }
}
