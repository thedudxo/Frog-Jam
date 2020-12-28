using Frog.Life;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frog
{
    public class Frog : MonoBehaviour
    {
        [Header("unrefactored")]
        //unrefactored stuff
        [SerializeField] FrogBloodSplater frogMetaBloodSplater;
        [SerializeField] FrogDynamicEffects frogDynamicEffects;
        [SerializeField] public Wave wave;


        LifeController lifeController;
        [HideInInspector] public VfxManager vfxManager;
        Controlls controlls;
        CameraTracker cameraController;


        [Header("Camera")]
        [SerializeField] Camera playerCamera;

        [Header("vfx")]
        public ParticleSystem respawnParticles;
        public ParticleSystem deathParticles;
        public List<GameObject> visuals;
        public Image bloodSplatter;

        [Header("animation")]
        public Animator animator;

        [Header("Sprites")]
        public SpriteRenderer spriteRenderer;
        public Sprite jumpSprite;
        public Sprite restSprite;

        [Header("UI")]
        public Slider powerBar;

        [Header("audio")]
        public AudioClip deathSounds;
        public AudioClip respawnSounds;
        public AudioClip jumpSounds;
        public AudioClip landSounds;

        //Initalised
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] new public Collider2D collider;
        [HideInInspector] public Transform CameraTransform { get; private set; }
        [HideInInspector] public CameraTarget CameraTarget { get; private set; }

        private void Initalise()
        {
            CameraTarget = new CameraTarget(transform);
            cameraController = new CameraTracker(this);
            vfxManager = new VfxManager(this);
            lifeController = new LifeController(this);
            controlls = new Controlls(this);
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
            vfxManager.Update();
            controlls.Update();
        }

        private void FixedUpdate()
        {
            cameraController.MoveTowardsTarget();
        }


        [HideInInspector] public List<GameObject> currentCollisions = new List<GameObject>();
        private void OnTriggerEnter2D(Collider2D collision) { currentCollisions.Add(collision.gameObject); }
        private void OnTriggerExit2D(Collider2D collision) { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionExit2D(Collision2D collision) { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            currentCollisions.Add(collision.gameObject);
            controlls.OnCollisionEnter2D();
        }


        public void RestartLevel() { lifeController.Restart(); }
    }
}
