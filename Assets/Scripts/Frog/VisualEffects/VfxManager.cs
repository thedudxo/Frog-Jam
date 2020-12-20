using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog
{
    public class VfxManager
    {
        Animator animator;
        Transform transform;

        List<GameObject> visuals;

        ParticleSystem respawnParticles;
        ParticleSystem deathParticles;
        const int respawnEmit = 5;
        const int deathEmit = 25;

        FrogBloodSplater bloodSplater;

        public VfxManager(Frog frog)
        {
            this.animator = frog.animator;
            this.transform = frog.transform;
            this.respawnParticles = frog.respawnParticles;
            this.deathParticles = frog.deathParticles;
            this.visuals = frog.visuals;

            bloodSplater = new FrogBloodSplater(frog.bloodSplatter);
        }

        public void Update()
        {
            bloodSplater.Update();
        }

        public void DeathEffects()
        {
            animator.SetTrigger("died");

            deathParticles.gameObject.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                deathParticles.transform.position.z);

            deathParticles.Emit(deathEmit);

            bloodSplater.StartSplatter();
        }

        public void RespawnEffects()
        {
            ShowFrogVisuals(true);

            respawnParticles.gameObject.transform.position = new Vector3(
                transform.position.x, 
                transform.position.y, 
                respawnParticles.transform.position.z);

            respawnParticles.Emit(respawnEmit);
        }

        public void ShowFrogVisuals(bool show)
        {
            foreach (GameObject visual in visuals)
            {
                visual.SetActive(show);
            }
        }
    }
}
