using UnityEngine;

public class SelfRightingObject : MonoBehaviour
{
    #region formulas
    /*
    torque = force * lever length 
    torque = force * 1 https://docs.unity3d.com/ScriptReference/Rigidbody2D.AddTorque.html
    torque = force
    torque = m(r^2)α https://courses.lumenlearning.com/boundless-physics/chapter/dynamics/
    torque = mass * (1^2) * αngularAcceleration
    torque = mass * αngularAcceleration https://courses.lumenlearning.com/physics/chapter/10-1-angular-acceleration/
    torque = mass * angularVelocityRadians / Time
    time = ( mass * angularVelocityRadians ) / torque
    */
    #endregion

    [SerializeField] Rigidbody2D rb;

    const float desiredRads = 0;
    const float maxTourqe = 5f;
    const float pi = Mathf.PI;

    float currentRadians;
    float radsToUpright;
    float desiredDirection;
    float angularMomentum;
    float timeToStop;
    float appliedTorque;
    float torqueToUprightNow;
    bool notUpright;

    private void FixedUpdate()
    {
        CalculateVariables();

        if (notUpright)
        {
            if (NotGoingToOvershoot)
                SpeedUp();

            else
                SlowDown();
        }
        else if (Moving)
        {
            Stop();
        }
    }

    private void CalculateVariables()
    {
        currentRadians = (rb.transform.rotation.eulerAngles.z % 360) * Mathf.Deg2Rad;

        RadsToUpright();
        DesiredDirection();
        angularMomentum                  = rb.mass * rb.angularVelocity * Mathf.Deg2Rad;
        timeToStop                       = Mathf.Abs(angularMomentum / maxTourqe);
        TourqeToUprightNow();
        appliedTorque                    = Mathf.Min(torqueToUprightNow, maxTourqe);
        DecideIfUpright();
    }

    private void RadsToUpright()
    {
        bool onLeftSide = currentRadians < pi;

        if (onLeftSide)
            radsToUpright = desiredRads - currentRadians;
        else
        {
            float radsInverted = pi - (currentRadians % pi);
            radsToUpright = desiredRads + radsInverted;
        }
    }

    private void DesiredDirection()
    {
        if (currentRadians < pi) desiredDirection = 1;
        else desiredDirection = -1;
    }


    private void TourqeToUprightNow()
    {
        float fixedTimeStepSqr = Mathf.Pow(Time.fixedDeltaTime, 2);
        float acceleration = radsToUpright / fixedTimeStepSqr;
        torqueToUprightNow = Mathf.Abs(rb.mass * acceleration);
    }


    private void DecideIfUpright()
    {
        const float tolerance = 0.1f;

        bool withinUpperTolerance = currentRadians < desiredRads + tolerance;
        bool withinLowerTolerance = currentRadians > desiredRads - tolerance;

        notUpright = !(withinUpperTolerance && withinLowerTolerance);
    }

    private void SpeedUp()
    {
        rb.AddTorque(appliedTorque * desiredDirection);
    }

    private void SlowDown()
    {
        rb.AddTorque(appliedTorque * -desiredDirection);
    }

    private void Stop()
    {
        // torque = mass * angularVelocityRadians / Time
        float tourqueToStop = angularMomentum / Time.fixedDeltaTime;
        rb.AddTorque(-tourqueToStop);
    }

    bool Moving => rb.angularVelocity > 1;
    bool NotGoingToOvershoot => (TimeUntillUpright() <= timeToStop);

    private float TimeUntillUpright()
    {
        //v  = d/t      vt = d      t  = d/v
        float angularVelocityRads = rb.angularVelocity * Mathf.Deg2Rad;
        float time = radsToUpright / angularVelocityRads;

        bool movingTowardsUpright = !((radsToUpright >= 0) ^ (angularVelocityRads >= 0));

        if (movingTowardsUpright) return time;
        else
        {
            float timeForFullRotation = 2 * pi / Mathf.Abs(angularVelocityRads);
            return timeForFullRotation - Mathf.Abs(time);
        }
    }
}
