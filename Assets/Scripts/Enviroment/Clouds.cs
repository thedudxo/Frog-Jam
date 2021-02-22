using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts {
    public class Clouds : MonoBehaviour
    {
        [SerializeField] public Level level;
        [SerializeField] List<Cloud> fluffyClouds = new List<Cloud>();
        [SerializeField] float respawnDistanceVariance;

    }
}
