using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances.Cameras
{

    public class CameraMovementByWeights : MonoBehaviour
    {
        [SerializeField] public new Camera camera;
        [SerializeField] Frog frog;


        Transform camTransform;
        const float Acceleration = 4f;


        List<ICameraWeight> weights;
        public TargetWeight targetWeight;


        void Start()
        {
            camTransform = camera.transform;

            targetWeight = new TargetWeight(camTransform, frog);

            weights = new List<ICameraWeight>()
            {
                new ClosestPursuerWeight(camTransform, frog),
                targetWeight
            };
        }

        private void FixedUpdate()
        {
            Vector3 DesiredPosition = ResultOfAllWeights();
            MoveTowards(DesiredPosition);
        }

        Vector3 ResultOfAllWeights()
        {
            Vector3 result = new Vector3();

            foreach(ICameraWeight weight in weights)
            {
                result += weight.Get();
            }

            return result;
        }

        public void MoveTowards(Vector3 desired)
        {
            Vector3 move = (desired - camTransform.position) * Acceleration;

            camTransform.position += move * Time.deltaTime;
        }
    }
}
