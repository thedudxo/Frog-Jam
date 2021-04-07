using UnityEngine;

namespace Util.Generic
{
    static class RandomUtil
    {
        public static float Vector2(Vector2 v) => Random.Range(v.x, v.y);
        public static float Variance(float average, float variance) => Random.Range(average - variance, average + variance);
    }
}
