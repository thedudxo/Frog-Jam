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

    public int AddFrog(Frog frog)
    {
        Frogs.Add(frog);
        return Frogs.Count - 1 ;
    }
}
