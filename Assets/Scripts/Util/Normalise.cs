using UnityEngine;

namespace Utils
{
    public static class Normalise
    {
        public static float Normalise01(float val, float max) => Mathf.Clamp(val / max, 0, 1);

        public static void ThrowExceptionIfNotNormal01 (float value)
        {
            if (value > 1 || value < 0)
                throw new System.ArgumentOutOfRangeException("jump01", "Must be between 0 and 1 inclusive");
        }

    }
}