using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Frogs.Instances.State
{
    public class FrogGhostState : FrogState, INotifyOnLeftPlatform
    {
        bool waitingToExitGhostMode = false;

        ContactFilter2D filter = new ContactFilter2D();

        const int defaultLayer = 0;

        struct Circle
        {
            public float radius;
            public Vector2 centre;
        }

        Circle overlapCircle;

        public FrogGhostState(FrogStateContext context) : base(context)
        {
            frog.events.SubscribeOnLeftPlatform(this);

            overlapCircle.radius = frog.collider.bounds.size.magnitude;

            filter.layerMask = GM.NoSelfCollisionsLayer;

            frog.gameObject.layer = GM.NoSelfCollisionsLayer;
            frog.controllers.vfx.GhostVisuals();
        }

        public override void UpdateState()
        {
            if (waitingToExitGhostMode)
            {
                TryLeaveGhostMode();
            }
        }

        public override void ExitState()
        {
            frog.gameObject.layer = defaultLayer;
            frog.controllers.vfx.UnGhostVisuals();
        }

        public void OnLeftPlatform() => waitingToExitGhostMode = true;


        void TryLeaveGhostMode()
        {
            var overlapColliders = new List<Collider2D>();
            GetCurrentOverlaps();
            
            if (AnyOverlappingFrogs() == false)
            {
                context.ChangeState(new FrogAliveState(context));
            }

            void GetCurrentOverlaps()
            {
                overlapCircle.centre = frog.collider.bounds.center;
                Physics2D.OverlapCircle(overlapCircle.centre, overlapCircle.radius, filter, overlapColliders);
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
    }
}