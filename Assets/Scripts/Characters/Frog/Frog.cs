using FrogScripts.Life;
using FrogScripts.Vfx;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;
using waveScripts;
using static FrogScripts.FrogLocationTracker;

namespace FrogScripts
{
    public class Frog : MonoBehaviour
    {
        [Header("make dynamic later")]
        [SerializeField] public Level currentLevel;
        [SerializeField] public Wave wave;

        [Header("External Managers")]
        [SerializeField] public FrogManager frogManager;
        [SerializeField] public SplitManager splitManager;

        [Header("Components")]
        [SerializeField] public VfxController vfxManager;
        [SerializeField] public LifeController lifeController;
        [SerializeField] public CameraController cameraController;
        [SerializeField] public JumpController jumpController;
        [SerializeField] public Controlls controlls;
        [SerializeField] public FrogCleanJumpManager cleanJumpEffectsManager;

        [Header("Player UI layer")]
        [SerializeField] public string UILayer;


        public Location location = Location.StartPlatform;
        FrogLocationTracker locationTracker;

        private void Awake()
        {
            frogManager.AddFrog(this);
            locationTracker = new FrogLocationTracker(this);
        }

        private void Update()
        {
            locationTracker.CheckLocation();
        }

        public void SetObjectUILayer(GameObject obj)
        {

            foreach(Transform child in obj.transform)
                setLayer(child.gameObject);

            setLayer(obj);

            void setLayer(GameObject _obj)
            {
                _obj.layer = LayerMask.NameToLayer(UILayer);
            }

        }

        public void Respawn()
        {
            jumpController.Respawn();
        }

        public void RestartLevel() //might be a bit sphagetti
        {
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

        [HideInInspector] public List<INotifyOnDeath> toNotifyOnDeath = new List<INotifyOnDeath>();
        [HideInInspector] public List<INotifyOnSetback> toNotifyOnSetback = new List<INotifyOnSetback>();
        [HideInInspector] public List<INotifyOnRestart> toNotifyOnRestart = new List<INotifyOnRestart>();
        [HideInInspector] public List<INotifyOnRestart> UnsubscribeToNotifyOnRestart = new List<INotifyOnRestart>();
        [HideInInspector] public List<INotifyBeforeRestart> toNotifyBeforeRestart = new List<INotifyBeforeRestart>();
        [HideInInspector] public List<INotifyOnAnyRespawn> toNotifyOnAnyRespawn = new List<INotifyOnAnyRespawn>();
        [HideInInspector] public List<INotifyOnEndLevel> toNotifyOnEndLevel = new List<INotifyOnEndLevel>();

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
            UnsubscribeToNotifyOnRestart.Add(subscriber);
        }

        public void SubscribeBeforeRestart(INotifyBeforeRestart subscriber)
        {
            toNotifyBeforeRestart.Add(subscriber);
        }
        public void UnsubscribeBeforeRestart(INotifyBeforeRestart subscriber)
        {
            toNotifyBeforeRestart.Remove(subscriber);
        }

        public void SubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Add(subscriber);
        }
        public void UnsubscribeOnAnyRespawn(INotifyOnAnyRespawn subscriber)
        {
            toNotifyOnAnyRespawn.Remove(subscriber);
        }

        public void SubscribeOnEndLevel(INotifyOnEndLevel subscriber)
        {
            toNotifyOnEndLevel.Add(subscriber);
        }
        public void UnsubscribeOnEndLevel(INotifyOnEndLevel subscriber)
        {
            toNotifyOnEndLevel.Remove(subscriber);
        }
        #endregion
    }
}
