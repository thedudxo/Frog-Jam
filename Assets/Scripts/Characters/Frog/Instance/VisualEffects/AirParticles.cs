﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts.Vfx
{
    public class AirParticleController
    {
        ParticleSystem airParticles;

        const float emitRate = 10;
        const float AirEffects_MaxVelocity = 12f; 
        const float AirEffects_MinVelocity = 7.5f;

        ParticleSystem.MainModule particleMain;
        ParticleSystem.EmissionModule particleEmission;
        ParticleSystem.ShapeModule particleShape;

        VfxController vfx;
        Rigidbody2D rb;

        public AirParticleController(VfxController vfx)
        {
            airParticles = vfx.airParticles;
            rb = vfx.rb;

            particleMain = airParticles.main;
            particleEmission = airParticles.emission;
            particleShape = airParticles.shape;
        }

        public void Update()
        {
            EmissionByVelocity();
            MatchRBTravelDirection();
        }

        private void EmissionByVelocity()
        {
            float minEffects = (rb.velocity.magnitude - AirEffects_MinVelocity);
            float maxEffects = (AirEffects_MaxVelocity - AirEffects_MinVelocity);
            float effectsNrml = Mathf.Clamp01(minEffects / maxEffects);
            particleEmission.rateOverTime = emitRate * effectsNrml;
        }

        private void MatchRBTravelDirection()
        {
            //using sohcatoa
            float angle = Mathf.Atan2(rb.velocity.x, -rb.velocity.y) * Mathf.Rad2Deg;
            particleShape.rotation = new Vector3(0, 0, angle - 90);
        }
    }
}
