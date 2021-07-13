using UnityEngine;

namespace Utils.Generic
{
    public static class TimerUtil
    {
        public static bool UpdateTick(ref float timer, float maxTime)
        {
            timer += Time.deltaTime;

            if (timer > maxTime)
            {
                return true;
            }

            return false;
        }
    }
}