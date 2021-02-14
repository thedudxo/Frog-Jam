using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;
using System.Linq;

public class RememberCollisions : MonoBehaviour, INotifyOnAnyRespawn
{
    public Dictionary<Frog, bool> FrogsCollided { get; private set; } = new Dictionary<Frog, bool>();

    private void Start()
    {
        foreach (Frog frog in FrogsCollided.Keys)
            frog.SubscribeOnAnyRespawn(this);
    }

    int ID(Collision2D collision) => collision.gameObject.GetInstanceID();
    int ID(Frog frog) => frog.gameObject.GetInstanceID();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool isPlayer = collision.gameObject.tag == GM.playerTag;
        if (isPlayer)
        {
            foreach (Frog frog in FrogsCollided.Keys.ToList())
            {
                if (ID(collision) == ID(frog))
                    FrogsCollided[frog] = true;
            }
        }
    }

    public void OnAnyRespawn()
    {
        // can't modify a collection while looping over it, so makes a list of frogs first.
        foreach (Frog frog in FrogsCollided.Keys.ToList())
            FrogsCollided[frog] = false;
    }
}
