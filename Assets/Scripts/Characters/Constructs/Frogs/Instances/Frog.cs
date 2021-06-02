using System.Collections.Generic;
using UnityEngine;
using Levels;
using Frogs.Collections;

namespace Frogs.Instances
{
    public class Frog : MonoBehaviour
    {
        [HideInInspector] public Level currentLevel;
        [HideInInspector] public FrogCollection collection;
        [HideInInspector] public SplitManager splitManager;

        [Header("Components")]
        [SerializeField] public new Collider2D collider;
        [SerializeField] public Rigidbody2D rb;

        [SerializeField] public Controllers controllers;
        [SerializeField] public FrogRunner FrogRunner;

        public FrogStateInfo state;

        public FrogEvents events = new FrogEvents();


        public float SetbackDistance { get; set; } = 25;
        public Vector2 spawnpoint;

        [Header("Player UI layer")]
        [SerializeField] public string UILayer;

        private void Awake()
        {
            FrogStartSettings.factory.SetupFrog(this);
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
