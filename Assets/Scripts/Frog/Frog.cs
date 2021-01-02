using FrogScripts.Life;
using FrogScripts.Vfx;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace FrogScripts
{
    public class Frog : MonoBehaviour
    {
        [Header("unrefactored")]
        [SerializeField] public Wave wave;

        [Header("make dynamic later")]
        [SerializeField] public Level currentLevel;

        [Header("Components")]
        [SerializeField] public FrogManager frogManager;
        [SerializeField] public VfxController vfxManager;
        [SerializeField] public LifeController lifeController;
        [SerializeField] public CameraController cameraController;
        [SerializeField] public Controlls controlls;

        private void Awake()
        {
            frogManager.AddFrog(this);
        }

        public void Respawn()
        {
            controlls.Respawn();
        }

        [HideInInspector] public List<GameObject> currentCollisions = new List<GameObject>();
        private void OnTriggerEnter2D(Collider2D collision) { currentCollisions.Add(collision.gameObject); }
        private void OnTriggerExit2D(Collider2D collision) { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionExit2D(Collision2D collision) { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            currentCollisions.Add(collision.gameObject);
        }


        public void RestartLevel() { lifeController.Restart(); }
    }
}
