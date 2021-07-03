using System.Collections.Generic;
using UnityEngine;


namespace Movements
{
    public class MovementByWeights
    {
        Transform transformToMove;
        readonly List<IVector3Weight> weights;
        readonly float Acceleration = 4f;

        public MovementByWeights(Transform transformToMove, List<IVector3Weight> weights,
            float Acceleration = 4f)
        {
            this.weights = weights;
            this.transformToMove = transformToMove;
            this.Acceleration = Acceleration;
        }

        public void Move()
        {
            Vector3 DesiredPosition = GetTotalWeight();
            MoveTowards(DesiredPosition);
        }

        Vector3 GetTotalWeight()
        {
            Vector3 result = new Vector3();

            foreach (IVector3Weight weight in weights)
            {
                result += weight.Get();
            }

            return result;
        }

        public void MoveTowards(Vector3 desiredPosition)
        {
            var currentPosition = transformToMove.position;

            Vector3 move = (desiredPosition - currentPosition) * Acceleration;

            transformToMove.position += move * Time.deltaTime;
        }
    }
}
