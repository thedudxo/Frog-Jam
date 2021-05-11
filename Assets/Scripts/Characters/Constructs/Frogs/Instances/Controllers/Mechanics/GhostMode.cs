using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Frogs.Instances
{
    public class GhostMode : MonoBehaviour, INotifyOnRestart, INotifyOnLeftPlatform
    {
        [SerializeField] Frog frog;

        bool waitingToExitGhostMode = false;

        ContactFilter2D filter = new ContactFilter2D();

        const int defaultLayer = 0;

        struct Circle
        {
            public float radius;
            public Vector2 centre;
        }

        Circle overlapCircle;

        void Start()
        {
            frog.events.SubscribeOnRestart(this);
            frog.events.SubscribeOnLeftPlatform(this);

            overlapCircle.radius = frog.collider.bounds.size.magnitude;

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

        public void OnRestart() => EnterGhostMode();
        public void OnLeftPlatform() => TryLeaveGhostMode();
        void EnterGhostMode()

        {
            frog.gameObject.layer = GM.NoSelfCollisionsLayer;
            frog.controllers.vfx.GhostVisuals();
        }

        void LeaveGhostMode()
        {
            frog.gameObject.layer = defaultLayer;
            frog.controllers.vfx.UnGhostVisuals();
        }

        void TryLeaveGhostMode()
        {
            var overlapColliders = new List<Collider2D>();
            FillWithCurrentOverlaps(overlapColliders);
            
            if (AnyOverlappingFrogs())
            {
                waitingToExitGhostMode = true;
            }

            else
            {
                waitingToExitGhostMode = false;
                LeaveGhostMode();
            }


            void FillWithCurrentOverlaps(List<Collider2D> ResultsList)
            {
                overlapCircle.centre = frog.collider.bounds.center;
                Physics2D.OverlapCircle(overlapCircle.centre, overlapCircle.radius, filter, ResultsList);
            }

            bool AnyOverlappingFrogs()
            {
                var overlapFrogQuery =
                    from collider in overlapColliders
                    let _frog = frog.collection.GetFrogComponent(collider.gameObject)
                    where _frog != null
                    where _frog != this.frog
                    select _frog;

                if (overlapFrogQuery.Any())
                    return true;
                else
                    return false;
            }
        }

        //use this debug to visualise where the collision check is happening
        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawSphere(frog.collider.bounds.center, overlapCircle.radius);
        //}

    }
}