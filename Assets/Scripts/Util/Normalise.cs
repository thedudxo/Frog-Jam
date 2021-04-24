using UnityEngine;

namespace Util
{
    public static class Normalise
    {
        public static float Normalise01(float val, float max) => Mathf.Clamp(val / max, 0, 1);

    }
}