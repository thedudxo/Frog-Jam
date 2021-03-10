using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;
using LevelScripts;

public static class SingletonThatNeedsToBeRemoved
{
    public static Frog frog;
}

public class FrogManager : MonoBehaviour
{
    [SerializeField] public List<Frog> Frogs { get; private set; } = new List<Frog>();
    [SerializeField] public Level level;
    [HideInInspector] public FrogManagerEvents events = new FrogManagerEvents();
    [HideInInspector] public WaveFrogMediatior waveMediator;

    public Dictionary<int, Frog> IDFrogs = new Dictionary<int, Frog>();

    private void Awake()
    {
        waveMediator = level.waveFrogMediatior;
    }

    public void AddFrog(Frog frog)
    {
        Frogs.Add(frog);
        IDFrogs.Add(frog.gameObject.GetInstanceID(), frog);
    }

    public Frog GetFrogComponent(GameObject obj)
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

    public Frog GetLastFrog()
    {
        Frog last = null;
        foreach(Frog frog in Frogs)
        {
            if (last == null)last = frog;
            else
            {
                if (frog.transform.position.x < last.transform.position.x)
                    last = frog;
            }
        }
        return last;
    }
}
