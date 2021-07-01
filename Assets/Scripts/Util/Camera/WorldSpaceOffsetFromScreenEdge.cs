using System;
using UnityEngine;

namespace Utils.Cameras
{
    public interface IScreen
    {
        float ScreenWidth { get; }
        Vector3 ScreenToWorld(Vector3 screenPos);
    }

    public static class ScreenUtil
    {
        public static float WorldOffsetFromScreenEdge(
            this IScreen cameraDecouple, float offsetPercent)
        {
            bool OffsetOutOfRange = offsetPercent > 1 || offsetPercent < 0;
            if (OffsetOutOfRange) throw new ArgumentOutOfRangeException("offsetPercent");

            float offsetPosition = cameraDecouple.ScreenWidth * offsetPercent;
            var screenPos = new Vector3(offsetPosition, 0, 0);
            var worldPos = cameraDecouple.ScreenToWorld(screenPos);

            return worldPos.x;
        }
    }
}
