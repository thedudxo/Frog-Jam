using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;

public static class SingletonThatNeedsToBeRemoved
{
    public static Frog frog;
}

public class FrogManager : MonoBehaviour
{
    [SerializeField] public List<Frog> Frogs { get; private set; } = new List<Frog>();

    public Dictionary<int, Frog> IDFrogs = new Dictionary<int, Frog>();

    public void AddFrog(Frog frog)
    {
        Frogs.Add(frog);
        IDFrogs.Add(frog.gameObject.GetInstanceID(), frog);
    }

    public Frog GetFrogFromGameobject(GameObject obj)
    { 
        if (obj.gameObject.CompareTag(GM.playerTag))
        {
            int collisionID = obj.gameObject.GetInstanceID();

            if (IDFrogs.TryGetValue(collisionID, out Frog frog))
            {
                return frog;
            }
        }
        return null;
    }

    public bool FrogIsFirst(Frog givenFrog)
    {
        if (! Frogs.Contains(givenFrog))
        {
            Debug.LogError("Given frog not managed by this", this);
        }

        bool isFirst = true;

        foreach (Frog frog in Frogs)
        {
            if(frog != givenFrog)
            {
                bool givenFrogIsBehind = frog.transform.position.x > givenFrog.transform.position.x;
                if (givenFrogIsBehind)
                {
                    isFirst = false;
                }
            }
        }

        return isFirst;
    }
}
