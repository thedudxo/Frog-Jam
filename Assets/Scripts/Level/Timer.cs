using UnityEngine;

namespace Level
{
    public class Timer
    {
        public float Time  { get; private set; } = 0;
        public float? PbTime { get; private set; } = null;

        public void Update()
        {
            Time += UnityEngine.Time.deltaTime;
        }

        public void CheckForNewPB()
        {
            PbTime = Mathf.Min(PbTime ?? float.MaxValue, Time);
        }

        public void Reset()
        {
            Time = 0;
        }
    }
}
