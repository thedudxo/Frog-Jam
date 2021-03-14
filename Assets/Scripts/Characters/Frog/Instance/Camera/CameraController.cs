using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts {
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] Frog frog;

        [HideInInspector] public CameraTarget target;

        Vector2 targetPos;
        Vector2 centerOffset;
        Transform camTransform;

        const float Acceleration = 4f;

        const float maxY = -1.85f;

        const float WaveOffsetWeight = 4;
        const float WaveMinDist = 20;
        const float waveMaxDist = 40;

        void Start()
        {
            camTransform = camera.transform;

            target = new CameraTarget(transform);
            Vector3 targetStart = target.GetPos();

            centerOffset = (camTransform.position - targetStart);
        }

        private void Update()
        {
            targetPos = target.GetPos();
        }

        private void FixedUpdate()
        {
            MoveTowardsTarget();
        }

        float WaveTrackOffsetX
        {
            get
            {
                if (frog.waveInteractions.AttachedWave == null) return 0;

                float 
                    wavePosX = frog.waveInteractions.AttachedWave.transform.position.x,
                    waveDistToFrog = camTransform.position.x - wavePosX,
                    waveDistanceXNormal = 1 - Mathf.Clamp01(
                        (waveDistToFrog - WaveMinDist) / (waveMaxDist - WaveMinDist));

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
