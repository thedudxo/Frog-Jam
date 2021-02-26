using UnityEngine;

namespace FrogScripts
{
    public class LevelStats : MonoBehaviour
    {
        [SerializeField] FrogTime frogTime;

        [HideInInspector] public int deaths;
        private int PbDeaths;

        public float? PbTime { get; private set; } = null;

        void Start()
        {
            PbDeaths = 999999999;
        }

        public void CheckForPBTime()
        {
            PbTime = Mathf.Min(PbTime ?? float.MaxValue, frogTime.CurrentLevelTime);
        }

        public int GetPbDeaths()
        {
            PbDeaths = Mathf.Min(PbDeaths, deaths);
            return PbDeaths;
        }
    }
}
