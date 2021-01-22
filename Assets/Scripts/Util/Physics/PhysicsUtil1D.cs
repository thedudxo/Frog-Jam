using UnityEngine;
using System.Collections;

public static class PhysicsUtil1D
{

    public static int DirectionToPosition(float from, float to)
    {
        if (from > to) return -1;
        else return 1;
    }

    public static float Direction(float velocity)
    {
        if (velocity > 0) return 1;
        else if (velocity < 0) return -1;
        else return float.NaN;
    }

    public static float ForceToStop(float mass, float velocity)
    {
        float force = mass * (velocity / Time.fixedDeltaTime);
        return force;
    }

    public static float TimeToPos(float directionalDistance, float velocity)
    {
        if (velocity == 0) return float.PositiveInfinity;

        bool velocityPositive = velocity > 0;
        bool distancePositive = directionalDistance > 0;
        bool gettingCloser = !(velocityPositive ^ distancePositive);

        if (gettingCloser)
        {
            float time = directionalDistance / velocity;
            return time;
        }
        else
        {
            return float.PositiveInfinity;
        }

    }

    public static float TimeToStop(float velocity, float acceleration)
    {
        return Mathf.Abs(velocity) / Mathf.Abs(acceleration);
    }

    public static bool AtPosition(float currentPosition, float desiredPosition, float tolerance)
    {
        //Debug.Log(currentPosition+" , "+(desiredPosition + tolerance));

        bool inUpperBound = currentPosition < desiredPosition + tolerance;
        bool inLowerBound = currentPosition > desiredPosition - tolerance;

        //Debug.Log(
        //       "In Upper Bound: " + inUpperBound +
        //    "\n In Lower Bound: " + inLowerBound);

        return inUpperBound && inLowerBound;
    }

    public static bool WillOvershoot(float currentPos, float targetPos, float velocity, float maxAcceleration)
    {
        float timeToStop = TimeToStop(velocity, maxAcceleration);

        float distance =  targetPos - currentPos;
        float timeToPos = TimeToPos(distance, velocity);

        bool willOvershoot = timeToStop > timeToPos;

        //Debug.Log(
        //    "willOvershoot: " + willOvershoot + 
        //    "\n timeToStop: " + timeToStop +
        //    "\n timeToPos: "  + timeToPos);
        if (willOvershoot) Debug.Log("Overshooting");
        return willOvershoot;

    }
}
