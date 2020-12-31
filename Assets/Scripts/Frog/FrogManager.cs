using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frog;

public static class SingletonThatNeedsToBeRemoved
{
    public static FrogController frog;
}

public static class FrogManager
{
    static List<FrogController> Frogs = new List<FrogController>();

    static public void AddFrog(FrogController frog)
    {
        Frogs.Add(frog);
    }
}
