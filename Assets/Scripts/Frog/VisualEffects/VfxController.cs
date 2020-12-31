using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts.Vfx
{
    public class VfxController : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] public Rigidbody2D rb;

        [Header("Particles")]
        [SerializeField] public ParticleSystem respawnParticles;
        [SerializeField] public ParticleSystem deathParticles;
        [SerializeField] public ParticleSystem airParticles;

        [Header("visuals")]
        [SerializeField] public List<GameObject> visuals;
        [SerializeField] public Image bloodSplatterImage;

        const int respawnEmit = 5;
        const int deathEmit = 25;

        ImageFadeout bloodSplaterController;
        AirParticleController airParticleController;

        public void Start()
        {

            bloodSplaterController = new ImageFadeout(bloodSplatterImage);
            airParticleController = new AirParticleController(this);
        }

        public void Update()
        {
            bloodSplaterController.Update();
            airParticleController.Update();
        }

        public void DeathEffects()
        {
            animator.SetTrigger("died");

            deathParticles.gameObject.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                deathParticles.transform.position.z);

            deathParticles.Emit(deathEmit);

            bloodSplaterController.StartFade();
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
