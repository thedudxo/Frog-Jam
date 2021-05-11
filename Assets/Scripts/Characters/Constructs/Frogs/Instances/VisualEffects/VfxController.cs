using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frogs.Instances.Visuals
{
    public class VfxController : MonoBehaviour, INotifyOnAnyRespawn, INotifyOnDeath
    {
        [SerializeField] Frog frog;
        [SerializeField] Animator animator;
        [SerializeField] public Rigidbody2D rb;

        [Header("Particles")]
        [SerializeField] public ParticleSystem respawnParticles;
        [SerializeField] public ParticleSystem deathParticles;
        [SerializeField] public ParticleSystem airParticles;

        [Header("visuals")]
        [SerializeField] public List<GameObject> visuals;
        [SerializeField] List<SpriteRenderer> sprites;
        [SerializeField] public Image bloodSplatterImage;


        const int respawnEmit = 5;
        const int deathEmit = 25;

        ImageFadeout bloodSplaterController;
        AirParticleController airParticleController;

        public void Start()
        {
            frog.events.SubscribeOnAnyRespawn(this);
            frog.events.SubscribeOnDeath(this);
            bloodSplaterController = new ImageFadeout(bloodSplatterImage);
            airParticleController = new AirParticleController(this);
        }

        public void Update()
        {
            bloodSplaterController.Update();
            airParticleController.Update();
        }

        const float ghostAlpha = 0.5f;
        public void GhostVisuals() => SetAlpha(ghostAlpha);
        public void UnGhostVisuals() => SetAlpha(1);
        void SetAlpha(float alpha)
        {
            foreach (SpriteRenderer sprite in sprites)
            {
                var newColour = sprite.color;
                newColour.a = alpha;
                sprite.color = newColour;
            }
        }



        public void OnDeath()
        {
            animator.SetTrigger("died");

            deathParticles.gameObject.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                deathParticles.transform.position.z);

            deathParticles.Emit(deathEmit);

            bloodSplaterController.StartFade();
        }

        public void OnAnyRespawn()
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
