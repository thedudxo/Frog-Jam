using UnityEngine;

public class SelfRightingObject : MonoBehaviour
{
    /* Formulas
    torque = force * lever length 
    torque = force * 1 https://docs.unity3d.com/ScriptReference/Rigidbody2D.AddTorque.html
    torque = force
    torque = m(r^2)α https://courses.lumenlearning.com/boundless-physics/chapter/dynamics/
    torque = mass * (1^2) * αngularAcceleration
    torque = mass * αngularAcceleration https://courses.lumenlearning.com/physics/chapter/10-1-angular-acceleration/
    torque = mass * angularVelocityRadians / Time
    time = ( mass * angularVelocityRadians ) / torque
    */

    [SerializeField] Rigidbody2D rb;

    const float desiredRads = 0;
    const float maxTourqe = 5f;
    const float pi = Mathf.PI;

    float currentRadians;
    float radsToUpright;
    float desiredDirection;
    float angularMomentum;
    float torqueToUprightNow;
    bool notUpright;

    private void FixedUpdate()
    {
        CalculateVariables();

        if (notUpright)
        {
            if (WillOvershoot())
                SlowDown();
            else
                SpeedUp();
            
        }
        else if (Moving)
        {
            SlowDown();
        }
    }

    private void CalculateVariables()
    {
        currentRadians = (rb.transform.rotation.eulerAngles.z % 360) * Mathf.Deg2Rad;

        RadsToUpright();
        DesiredDirection();
        angularMomentum                  = rb.mass * rb.angularVelocity * Mathf.Deg2Rad;
        TourqeToUprightNow();
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
        if (currentRadians < pi) desiredDirection = -1;
        else desiredDirection = 1;
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
        Debug.Log("speeding up");
        float appliedTorque = Mathf.Min(torqueToUprightNow, maxTourqe);
        rb.AddTorque(appliedTorque * desiredDirection);
    }

    private void SlowDown()
    {
        Debug.Log("Slowing Down");
        // torque = mass * angularVelocityRadians / Time
        float tourqueToStop = Mathf.Abs(angularMomentum) / Time.fixedDeltaTime;

        float appliedTorque = Mathf.Min(tourqueToStop, maxTourqe);

        float direction;
        if (rb.angularVelocity > 0) direction = 1;
        else direction = -1;

        rb.AddTorque(appliedTorque * -direction);
    }

    bool Moving => rb.angularVelocity > 1;

    private bool WillOvershoot()
    {
        if (angularMomentum == 0) return false;

        float timeUntillUpright = TimeUntillUpright();
        float timeToStop = Mathf.Abs(angularMomentum / maxTourqe);

        return timeUntillUpright <= timeToStop;
    }

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
