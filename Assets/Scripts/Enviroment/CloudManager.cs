using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts {

    public class CloudManager : MonoBehaviour
    {
        [SerializeField] public Region region;

        [SerializeField] public float averageSpeed = -0.02f;
        [SerializeField] public float speedVariance = 0.005f;

        [SerializeField] public bool randomPositions = true;
        [SerializeField] public Vector2 randomRespawnTime;

        [HideInInspector] public List<Cloud> Clouds = new List<Cloud>();
    }
}
