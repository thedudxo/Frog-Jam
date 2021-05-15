using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs.Instances;
using System.Linq;

public class RememberCollisions : MonoBehaviour, INotifyOnAnyRespawn
{
    public Dictionary<Frog, bool> FrogsCollided { get; private set; } = new Dictionary<Frog, bool>();

    public void AddFrog(Frog frog)
    {
        if (FrogsCollided.ContainsKey(frog))
        {
            Debug.LogWarning("Tried adding frog again...", frog);
            Debug.LogWarning("...to remember collisions list", this);
            return;
        }
        FrogsCollided.Add(frog, false);
        frog.events.SubscribeOnAnyRespawn(this);
    }

    int ID(Collision2D collision) => collision.gameObject.GetInstanceID();
    int ID(Frog frog) => frog.gameObject.GetInstanceID();

    bool IsPlayer(Collision2D collision) => collision.gameObject.tag == GM.playerTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayer(collision))
        {
            RememberCollidingFrog(collision);
        }
    }

    void RememberCollidingFrog(Collision2D collision) 
    {
        foreach (Frog frog in FrogsCollided.Keys.ToList())
        {
            if (ID(collision) == ID(frog))
                FrogsCollided[frog] = true;
        }
    }

    public void OnAnyRespawn()
    {
        foreach (Frog frog in FrogsCollided.Keys.ToList())
            FrogsCollided[frog] = false;
    }
}
