using System.Collections.Generic;
using UnityEngine;
using static FrogDeathConditions;

public class FrogLifeState
{
    FrogRespawnTimer respawnTimer;
    FrogDeathConditions deathConditions;
    FrogRespawn respawn;
    FrogVfxManager vfxManager;

    Rigidbody2D rb;
    Collider2D collider;
    CameraTarget cameraTarget;
    Transform transform;

    bool alive = true;

    public FrogLifeState(Transform transform, Wave wave, Rigidbody2D rb, Collider2D collider, FrogVfxManager vfxManager, CameraTarget cameraTarget, List<GameObject> currentCollisions)
    {
        respawn = new FrogRespawn(transform.position, transform, wave);
        respawnTimer = new FrogRespawnTimer();
        deathConditions = new FrogDeathConditions(transform, currentCollisions);

        this.vfxManager = vfxManager;

        this.rb = rb;
        this.collider = collider;
        this.cameraTarget = cameraTarget;
        this.transform = transform;
    }

    DeathType deathType;

    public void Update()
    {
        if (GM.gameState == GM.GameState.finishedLevel) { return; }

        switch (alive)
        {
            case true:
                deathType = deathConditions.GetDeathType();
                if (deathType != DeathType.none)
                    { Die(); Debug.Log(deathType); }
                break;

            case false:
                if (respawnTimer.ShouldRespawnNow())
                    { Respawn(); }
                break;
        }
    }

    private void Respawn()
    {
        Debug.Log("respawn");
        if (deathType == DeathType.setback) respawn.Setback();
        else respawn.Restart();
        vfxManager.ShowFrogVisuals(true);
        alive = true;
        SetComponentsState(true);
    }

    void Die()
    {
        alive = false;
        SetComponentsState(false);
        Statistics.totalDeaths++;
        vfxManager.DeathEffects();
        GM.PhillDied();
    }

    public void SetComponentsState(bool alive)
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


}
