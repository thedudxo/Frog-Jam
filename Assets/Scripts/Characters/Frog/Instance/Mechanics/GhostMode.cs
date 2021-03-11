using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace FrogScripts
{
    public class GhostMode : MonoBehaviour, INotifyOnRestart, INotifyOnLeftPlatform
    {
        [SerializeField] Frog frog;

        bool waitingToExitGhostMode = false;

        float radius;
        ContactFilter2D filter = new ContactFilter2D();

        void Start()
        {
            frog.events.SubscribeOnRestart(this);
            frog.events.SubscribeOnLeftPlatform(this);

            radius = frog.collider.bounds.size.magnitude;

            filter.layerMask = GM.NoSelfCollisionsLayer;

            EnterGhostMode();
        }

        private void Update()
        {
            if (waitingToExitGhostMode)
            {
                TryLeaveGhostMode();
            }
        }

        private void OnDrawGizmos()
        {
            //use this to see where the collision check is happening
            //Gizmos.DrawSphere(frog.collider.bounds.center, radius);
        }

        public void OnRestart() => EnterGhostMode();
        public void OnLeftPlatform() => TryLeaveGhostMode();
        void EnterGhostMode()

        {
            frog.gameObject.layer = GM.NoSelfCollisionsLayer;
            frog.vfxManager.GhostVisuals();
        }

        void TryLeaveGhostMode()
        {
            Vector2 circleCenter = frog.collider.bounds.center;

            List<Collider2D> overlapColliders = new List<Collider2D>();
            
            //this fills the list with all colliders cuurently overlaping
            Physics2D.OverlapCircle(circleCenter, radius, filter, overlapColliders);

            var overlapFrogQuery =
                from collider in overlapColliders
                let _frog = frog.manager.GetFrogComponent(collider.gameObject)
                where _frog != null
                where _frog != this.frog
                select _frog;


            if (overlapFrogQuery.Any())
                waitingToExitGhostMode = true;
            else
            {
                waitingToExitGhostMode = false;
                LeaveGhostMode();
            }
        }

        void LeaveGhostMode()
        {
            frog.gameObject.layer = 0; //default layer
            frog.vfxManager.UnGhostVisuals();
        }
    }
}