using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels {

    public class CloudManager : MonoBehaviour
    {
        [SerializeField] Region _region;
        [SerializeField] Vector2 expandRegion;
        public Region region;

        [SerializeField] public float averageSpeed = -0.02f;
        [SerializeField] public float speedVariance = 0.005f;

        [SerializeField] public Vector2 randomRespawnTime;
        [SerializeField] public Vector2 randomLifeTime = new Vector2(10,20);


        [HideInInspector] public List<Cloud> Clouds = new List<Cloud>();

        private void Awake()
        {
            region = gameObject.AddComponent<Region>();
            region.start = _region.start - expandRegion.x;
            region.end = _region.end + expandRegion.y;
        }
    }
}
