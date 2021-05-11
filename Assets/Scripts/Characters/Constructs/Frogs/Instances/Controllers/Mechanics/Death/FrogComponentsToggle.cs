﻿using System.Collections;
using UnityEngine;

namespace Frogs.Instances.Deaths
{
    public class FrogComponentsToggle : MonoBehaviour
    {
        [SerializeField] Frog frog;
        Rigidbody2D rb;
        new CameraController camera;

        private void Start()
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
                camera.target.Set(frog.transform);
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                camera.target.Set(frog.transform.position);
            }
        }
    }
}