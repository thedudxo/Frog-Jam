using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogVfxManager
{
    Animator animator;
    Transform transform;

    List<GameObject> visuals;

    ParticleSystem respawnParticles;
    ParticleSystem deathParticles;
    const int respawnEmit = 5;
    const int deathEmit = 25;

    public FrogVfxManager(Animator animator, Transform transform, ParticleSystem respawnParticles, ParticleSystem deathParticles, List<GameObject> visuals)
    {
        this.animator = animator;
        this.transform = transform;
        this.respawnParticles = respawnParticles;
        this.deathParticles = deathParticles;
        this.visuals = visuals;
    }

    public void DeathEffects()
    {
        animator.SetTrigger("died");

        deathParticles.gameObject.transform.position = new Vector3(
            transform.position.x, 
            transform.position.y,
            deathParticles.transform.position.z);

        deathParticles.Emit(deathEmit);
    }

    public void ShowFrogVisuals(bool show )
    {
        foreach (GameObject visual in visuals)
        {
            visual.SetActive(show);
        }
    }
}
