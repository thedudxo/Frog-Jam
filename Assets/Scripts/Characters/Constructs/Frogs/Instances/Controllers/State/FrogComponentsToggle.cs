﻿using Frogs.Instances.Cameras;
using UnityEngine;

namespace Frogs.Instances.State
{
    public class FrogComponentsToggle : MonoBehaviour
    {
        [SerializeField] Frog frog;
        Rigidbody2D rb;
        new CameraMovementByWeights camera;

        private void Awake()
        {
            rb = frog.rb;
            camera = frog.controllers.camera;
        }

        public void ToggleComponents(bool alive)
        {
            frog.collider.enabled = alive;

            if (alive)
            {
                rb.velocity = new Vector2(frog.rb.velocity.x, 0);
                rb.gravityScale = 1;
                camera.targetWeight.target.Set(frog.transform);
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                camera.targetWeight.target.Set(frog.transform.position);
            }
        }
    }
}