﻿using System.Collections;
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
}
