using Frogs.Instances;
using Pursuits;
using UnityEngine;

namespace Movements
{
    class ClosestPursuerWeight : IVector3Weight
    {
        Transform cameraTransform;
        Frog frog;

        readonly float chaserOffsetWeight = 5;
        readonly float chaserMinDist = 20;
        const float chaserMaxDist = 40;

        public ClosestPursuerWeight(Transform cameraTransform, Frog frog)
        {
            this.cameraTransform = cameraTransform;
            this.frog = frog;
        }

        float CamPosX => cameraTransform.position.x;

        public Vector3 Get()
        {
            Pursuer firstPursuerBehind = frog.FrogRunner.runner?.pursuerBehind;
            if (firstPursuerBehind == null) return new Vector3();

            float
                pursuerPosx = firstPursuerBehind.position,
                pursuerDistToFrog = CamPosX - pursuerPosx,
                pursuerDistanceNormal = 1 - Mathf.Clamp01(
                    (pursuerDistToFrog - chaserMinDist) / (chaserMaxDist - chaserMinDist));

            float result = pursuerDistanceNormal * chaserOffsetWeight;
            return new Vector3(-result, 0, 0);
        }
    }
}
