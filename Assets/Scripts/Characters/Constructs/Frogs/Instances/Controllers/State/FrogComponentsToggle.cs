using Frogs.Instances.Cameras;
using UnityEngine;

namespace Frogs.Instances.State
{
    public class FrogComponentsToggle : MonoBehaviour
    {
        [SerializeField] Frog frog;
        Rigidbody2D rb;
        CameraMechanics cameraMovement;

        private void Awake()
        {
            rb = frog.rb;
            cameraMovement = frog.controllers.cameraMechanics;
        }

        public void ToggleComponents(bool alive)
        {
            frog.collider.enabled = alive;

            if (alive)
            {
                rb.velocity = new Vector2(frog.rb.velocity.x, 0);
                rb.gravityScale = 1;
                cameraMovement.Target.Set(frog.transform);
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                cameraMovement.Target.Set(frog.transform.position);
            }
        }
    }
}