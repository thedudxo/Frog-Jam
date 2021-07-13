using UnityEngine;

namespace Frogs.Instances.Jumps
{
    class GroundedDetection : MonoBehaviour
    {
        public bool IsGrounded => touching > 0;

        int touching = 0;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            touching++;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            touching--;
        }

    }
}
