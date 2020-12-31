using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrogScripts;

public static class SingletonThatNeedsToBeRemoved
{
    public static Frog frog;
}

public static class FrogManager
{
    static List<Frog> Frogs = new List<Frog>();

    static public void AddFrog(Frog frog)
    {
        Frogs.Add(frog);
    }


}
