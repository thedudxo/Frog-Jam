using FrogScripts.Life;
using FrogScripts.Vfx;
using FrogScripts.Jump;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;
using WaveScripts;
using static FrogScripts.FrogState;

namespace FrogScripts
{
    public class Frog : MonoBehaviour
    {
        [Header("make dynamic later")]
        [SerializeField] public Level currentLevel;

        [Header("External Managers")]
        [SerializeField] public FrogManager manager;
        [SerializeField] public SplitManager splitManager;

        [Header("Components")]
        [SerializeField] public new Collider2D collider;
        [SerializeField] public Rigidbody2D rb;
        [SerializeField] public VfxController vfxManager;
        [SerializeField] public LifeController lifeController;
        [SerializeField] public CameraController cameraController;
        [SerializeField] public JumpController jumpController;
        [SerializeField] public Controlls controlls;
        [SerializeField] public FrogCleanJumpManager cleanJumpEffectsManager;
        [SerializeField] public FrogWaveInteractions waveInteractions;

        public FrogState stateControlls;
        public State state => stateControlls.state;

        public FrogEvents events = new FrogEvents();

        [Header("Player UI layer")]
        [SerializeField] public string UILayer;

        [HideInInspector] public bool inDanger = false;


        private void Awake()
        {
            manager.AddFrog(this);
            stateControlls = new FrogState(this);
        }

        private void Update()
        {
            stateControlls.CheckLocation();
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

    }
}
