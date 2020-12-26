using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frog {
    public class CameraTracker
    {

        public CameraTarget target;
        Vector2 targetPos;
        Vector2 centerOffset;
        Transform camTransform;
        Transform waveTransform;

        const float Acceleration = 4f;

        const float maxY = -1.85f;

        const float WaveOffsetWeight = 4;
        const float WaveMinDist = 10;
        const float waveMaXDist = 30;

        public CameraTracker(Frog frog)
        {
            camTransform = frog.CameraTransform;
            waveTransform = frog.wave.transform;

            target = frog.CameraTarget;

            if (target == null) target = new CameraTarget();
            Vector3 targetStart = target.GetPos();

            centerOffset = (camTransform.position - targetStart);
        }

        public void Update()
        {
            targetPos = target.GetPos();
        }

        float WaveTrackOffsetX
        {
            get
            {
                float waveDistToFrog = camTransform.position.x - waveTransform.position.x;
                float waveDistanceXNormal = 1 - Mathf.Clamp01(
                    (waveDistToFrog - WaveMinDist) / (waveMaXDist - WaveMinDist));

               return waveDistanceXNormal * WaveOffsetWeight;
            }
        }


        public void MoveTowardsTarget()
        {

            float offsetTargetX = (targetPos.x + centerOffset.x);
            float offsetTargetY = (Mathf.Min(targetPos.y, maxY) + centerOffset.y);

            float moveX = ((offsetTargetX - camTransform.position.x) - WaveTrackOffsetX) * Acceleration;
            float moveY =  (offsetTargetY - camTransform.position.y)                     * Acceleration;


            camTransform.position = new Vector3(
                camTransform.position.x + (moveX) * Time.deltaTime,
                camTransform.position.y + (moveY) * Time.deltaTime,
                camTransform.position.z);
        }
    }
}
