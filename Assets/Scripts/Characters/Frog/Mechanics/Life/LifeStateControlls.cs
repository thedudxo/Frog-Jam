using UnityEngine;
using static FrogScripts.Life.DeathConditions;
using FrogScripts.Vfx;
using System.Collections.Generic;

namespace FrogScripts.Life
{
    class LifeStateControlls : MonoBehaviour
    {
        Vector2 levelStart;
        const float respawnSetBack = 25;
        const int respawnHeight = 5;

        [SerializeField] Frog frog;
        [SerializeField] VfxController vfx;
        [SerializeField] Rigidbody2D rb;
        [SerializeField] new Collider2D collider;
        [SerializeField] new Transform transform;

        [Header("audio")]
        [SerializeField]  public AudioClip deathSounds;
        [SerializeField]  public AudioClip respawnSounds;

        public void Start()
        {
            levelStart = transform.position;
        }

        public void Respawn(DeathType deathType)
        {
            if (deathType == DeathType.setback)
                Setback();
            else
                Restart();

            vfx.RespawnEffects();
            respawnSounds.PlayRandom();
            ToggleComponents(true);


            foreach (INotifyOnAnyRespawn notify in  frog.toNotifyOnAnyRespawn) notify.OnAnyRespawn();
        }

        public void Die()
        {
            ToggleComponents(false);
            Statistics.totalDeaths++;
            vfx.DeathEffects();
            deathSounds.PlayRandom();
            GM.gameMusic.DetuneMusic();

            foreach (INotifyOnDeath notify in frog.toNotifyOnDeath) notify.OnDeath();
        }

        void ToggleComponents(bool alive)
        {
            if (alive)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.gravityScale = 1;
                collider.enabled = true;
                frog.cameraController.target.Set(transform);
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                collider.enabled = false;
                frog.cameraController.target.Set(transform.position);
            }
        }

        bool FrogIsOnStartPlatform => transform.position.x < (levelStart.x + GM.currentLevel.startLength);

        public void Setback()
        {
            Vector2 respawnPosition = new Vector2(transform.position.x - respawnSetBack, respawnHeight);
            transform.position = respawnPosition;

            if (FrogIsOnStartPlatform)
            {
                Restart();
                return;
            }

            frog.wave.Setback(respawnSetBack);

            foreach (INotifyOnSetback notify in frog.toNotifyOnSetback) notify.OnSetback();
        }

        public void Restart()
        {
            rb.velocity = Vector3.zero;
            transform.position = levelStart;

            frog.wave.Restart();

            foreach (INotifyOnRestart notify in frog.toNotifyOnRestart) notify.OnRestart();
        }
    }
}
