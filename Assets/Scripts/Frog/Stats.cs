using UnityEngine;

namespace FrogScripts
{
    public class LevelStats
    {
        public int deaths;
        private int PbDeaths;

        public float Time { get; private set; } = 0;
        public float? PbTime { get; private set; } = null;

        public LevelStats()
        {
            PbDeaths = 999999999;
        }

        public void Update()
        {
            Time += UnityEngine.Time.deltaTime;
        }

        public void CheckForPBTime()
        {
            PbTime = Mathf.Min(PbTime ?? float.MaxValue, Time);
        }

        public void ResetTimer()
        {
            Time = 0;
        }

        public int GetPbDeaths()
        {
            PbDeaths = Mathf.Min(PbDeaths, deaths);
            return PbDeaths;
        }
    }
}
