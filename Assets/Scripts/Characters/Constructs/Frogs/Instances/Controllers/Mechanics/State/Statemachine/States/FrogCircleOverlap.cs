using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Frogs.Instances.State
{
    class FrogCircleOverlap
    {
        public float radius;
        public Vector2 centre;
        ContactFilter2D filter = new ContactFilter2D();
        Frog frog;

        public FrogCircleOverlap(Frog frog)
        {
            filter.layerMask = GM.NoSelfCollisionsLayer;
            this.frog = frog;
        }

        public bool IsOverlaping()
        {
            var overlapColliders = new List<Collider2D>();
            centre = frog.collider.bounds.center;
            Physics2D.OverlapCircle(centre, radius, filter, overlapColliders);


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