using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances {
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

        const float chaserOffsetWeight = 4;
        const float chaserMinDist = 20;
        const float chaserMaxDist = 40;

        void Start()
        {
            camTransform = camera.transform;

            target = new CameraTarget(transform);
            Vector3 targetStart = target.GetPos();

            centerOffset = (camTransform.position - targetStart);
        }

        public void SetLayerMask(LayerMask mask) => camera.cullingMask = mask;

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
                Pursuits.Pursuer firstPursuerBehind = frog.FrogRunner.runner?.pursuerBehind;
                if (firstPursuerBehind == null) return 0;

                float
                    chaserPos = firstPursuerBehind.position,
                    chaserDistToFrog = camTransform.position.x - chaserPos,
                    chaserDistanceXNormal = 1 - Mathf.Clamp01(
                        (chaserDistToFrog - chaserMinDist) / (chaserMaxDist - chaserMinDist));

               return chaserDistanceXNormal * chaserOffsetWeight;
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
