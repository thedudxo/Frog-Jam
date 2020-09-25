using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberCollisions : MonoBehaviour, IRespawnResetable
{

    [SerializeField] string collisionToRemember = GM.playerTag;
    public bool HasCollided { get; private set; } = false;

    private void Start()
    {
        GM.AddRespawnResetable(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == collisionToRemember)
        {
            HasCollided = true;
        }
    }

    public void PhillRespawned()
    {
        HasCollided = false;
    }
}
