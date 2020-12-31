using FrogScripts.Life;
using FrogScripts.Vfx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts
{
    public class Frog : MonoBehaviour
    {
        [Header("unrefactored")]
        [SerializeField] public Wave wave;

        [Header("Components")]
        public VfxController vfxManager;
        LifeController lifeController;
        Controlls controlls;
        CameraTracker cameraController;

        [Header("Camera")]
        [SerializeField] Camera playerCamera;

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

        [Header("Jumpbox")]
        public Transform groundedDetectionBox;

        //Initalised stuff
        [HideInInspector] public Rigidbody2D rb;
        [HideInInspector] new public Collider2D collider;
        [HideInInspector] public Transform CameraTransform { get; private set; }
        [HideInInspector] public CameraTarget CameraTarget { get; private set; }

        private void Initalise()
        {
            FrogManager.AddFrog(this);
            CameraTarget = new CameraTarget(transform);
            cameraController = new CameraTracker(this);
            lifeController = new LifeController(this);
            controlls = new Controlls(this);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            CameraTransform = playerCamera.transform;

            Initalise();
        }

        private void Update()
        {
            cameraController.Update();
            lifeController.Update();
            controlls.Update();
        }

        public void Respawn()
        {
            controlls.Respawn();
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
