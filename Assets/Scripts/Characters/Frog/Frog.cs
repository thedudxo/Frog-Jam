using FrogScripts.Life;
using FrogScripts.Vfx;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;

namespace FrogScripts
{
    public class Frog : MonoBehaviour
    {
        [Header("make dynamic later")]
        [SerializeField] public Level currentLevel;
        [SerializeField] public Wave wave;

        [Header("Components")]
        [SerializeField] public FrogManager frogManager;
        [SerializeField] public VfxController vfxManager;
        [SerializeField] public LifeController lifeController;
        [SerializeField] public CameraController cameraController;
        [SerializeField] public Controlls controlls;

        [Header("Initial Subscripions")]
        [SerializeField] public List<INotifyOnDeath> toNotifyOnDeath = new List<INotifyOnDeath>();
        [SerializeField] public List<INotifyOnSetback> toNotifyOnSetback = new List<INotifyOnSetback>();
        [SerializeField] public List<INotifyOnRestart> toNotifyOnRestart = new List<INotifyOnRestart>();
        [SerializeField] public List<INotifyOnAnyRespawn> toNotifyOnAnyRespawn = new List<INotifyOnAnyRespawn>();

        private void Awake()
        {
            frogManager.AddFrog(this);
        }

        public void Respawn()
        {
            controlls.Respawn();
        }

        public void RestartLevel() 
        {
            Debug.Log(wave, this);
            lifeController.Restart(); 
        }

        #region collisions
        [HideInInspector] public List<GameObject> currentCollisions = new List<GameObject>();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            currentCollisions.Add(collision.gameObject);
        }
        private void OnTriggerExit2D(Collider2D collision)
        { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionExit2D(Collision2D collision)
        { currentCollisions.Remove(collision.gameObject); }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            currentCollisions.Add(collision.gameObject);
        }
        #endregion

        #region events

        public void SubscribeOnDeath(INotifyOnDeath subscriber)
        {
            toNotifyOnDeath.Add(subscriber);
        }
        public void UnscubscribeOnDeath(INotifyOnDeath subscriber)
        {
            toNotifyOnDeath.Remove(subscriber);
        }

        public void SubscribeOnSetback(INotifyOnSetback subscriber)
        {
            toNotifyOnSetback.Add(subscriber);
        }
        public void UnsubscribeOnSetback(INotifyOnSetback subscriber)
        {
            toNotifyOnSetback.Remove(subscriber);
        }

        public void SubscribeOnRestart(INotifyOnRestart subscriber)
        {
            toNotifyOnRestart.Add(subscriber);
        }
        public void UnsubscribeOnRestart(INotifyOnRestart subscriber)
        {
            toNotifyOnRestart.Remove(subscriber);
        }

        public void SubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Add(subscriber);
        }
        public void UnsubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Remove(subscriber);
        }
        #endregion
    }
}
