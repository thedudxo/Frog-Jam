using FrogScripts.Life;
using FrogScripts.Vfx;
using FrogScripts.Jump;
using System.Collections.Generic;
using UnityEngine;
using LevelScripts;
using static FrogScripts.FrogState;
using Characters;

namespace FrogScripts
{
    public class Frog : MonoBehaviour
    {
        [HideInInspector] public Level currentLevel;
        [HideInInspector] public FrogCollection manager;
        [HideInInspector] public SplitManager splitManager;

        [Header("External")]
        [SerializeField] public PursuitHandler pursuitHandler;

        [Header("Components")]
        [SerializeField] public new Collider2D collider;
        [SerializeField] public Rigidbody2D rb;

        [SerializeField] public Controllers controllers;
        [SerializeField] public FrogRunner FrogRunner;

        public FrogState stateControlls;
        public State state => stateControlls.state;

        public FrogEvents events = new FrogEvents();

        public float SetbackDistance { get; set; } = 25;

        [Header("Player UI layer")]
        [SerializeField] public string UILayer;

        [HideInInspector] public bool inDanger = false;


        private void Awake()
        {
            manager = FrogStartSettings.frogCollection;
            currentLevel = FrogStartSettings.level;

            manager.AddFrog(this);
            splitManager = currentLevel.splitManager;
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

            void setLayer(GameObject _obj) => _obj.layer = LayerMask.NameToLayer(UILayer);
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
