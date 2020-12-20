using UnityEngine;
using static Frog.Life.DeathConditions;

namespace Frog.Life
{
    class State
    {
        public State(Frog frog)
        {
            this.frog = frog;
            this.rb = frog.rb;
            this.collider = frog.collider;
            this.cameraTarget = frog.CameraTarget;
            this.transform = frog.transform;

            levelStart = transform.position;
        }

        Vector2 levelStart;
        const float respawnSetBack = 25;
        const int respawnHeight = 5;

        Frog frog;
        Rigidbody2D rb;
        Collider2D collider;
        CameraTarget cameraTarget;
        Transform transform;

        public void Respawn(DeathType deathType)
        {
            if (deathType == DeathType.setback)
                Setback();
            else
                Restart();

            frog.VfxManager.ShowFrogVisuals(true);
            frog.respawnSounds.PlayRandom();
            ToggleComponents(true);
        }

        public void Die()
        {
            ToggleComponents(false);
            Statistics.totalDeaths++;
            frog.VfxManager.DeathEffects();
            frog.deathSounds.PlayRandom();
            GM.gameMusic.DetuneMusic();
            GM.PhillDied();
        }

        void ToggleComponents(bool alive)
        {
            if (alive)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.gravityScale = 1;
                collider.enabled = true;
                cameraTarget.Set(transform);
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
                collider.enabled = false;
                cameraTarget.Set(transform.position);
            }
        }

        bool FrogIsOnStartPlatform => transform.position.x < (levelStart.x + GM.currentLevel.spawnPlatformLength);

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
        }

        public void Restart()
        {
            transform.position = levelStart;
            frog.wave.Restart();
            GM.splitManager.currentTime = 0;
            GM.LevelRestart();
        }
    }
}
