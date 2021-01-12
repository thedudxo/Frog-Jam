using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRightingObject : MonoBehaviour
{
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

    [SerializeField] Rigidbody2D rb;

    const float uprightRads = 0;
    const float maxTourqe = 5f;
    const float pi = Mathf.PI;

    float currentRotation;
    float radsToUpright;
    float desiredDirection;
    float angularMomentum;
    float timeToStop;
    float appliedTorque;
    float torqueToUprightNow;

    bool upright;


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

    private void FindRadsToUpright()
    {
        if (currentRotation < 180) radsToUpright = uprightRads - (currentRotation * Mathf.Deg2Rad);
        else radsToUpright = uprightRads - (-(180 - (currentRotation % 180)) * Mathf.Deg2Rad);
    }

    private void FindDesiredDirection()
    {
        if (currentRotation < 180) desiredDirection = 1;
        else desiredDirection = -1;
    }

    private void FindAngularMomentum()
    {
        angularMomentum = rb.mass * rb.angularVelocity * Mathf.Deg2Rad;
    }

    private void FindTimeToStop()
    {
        timeToStop = Mathf.Abs(angularMomentum / maxTourqe);
    }

    private void FindTourqeToUprightNow()
    {
        float fixedTimeStepSqr = Mathf.Pow(Time.fixedDeltaTime, 2);
        float acceleration = radsToUpright / fixedTimeStepSqr;
        torqueToUprightNow = Mathf.Abs(rb.mass * acceleration);
    }

    private void DecideAppliedTorque()
    {
        appliedTorque = Mathf.Min(torqueToUprightNow, maxTourqe);
    }

    private void DecideIfUpright()
    {
        const float tolerance = 0.1f;

        bool withinUpperTolerance = currentRotation * Mathf.Deg2Rad < uprightRads + tolerance;
        bool withinLowerTolerance = currentRotation * Mathf.Deg2Rad > uprightRads - tolerance;

        upright = withinUpperTolerance && withinLowerTolerance;

    }

    private void CalculateVariables()
    {
        currentRotation = rb.transform.rotation.eulerAngles.z % 360;
        //float currentRotationRads = (rb.transform.rotation.eulerAngles.z % 360) * Mathf.Deg2Rad;

        FindRadsToUpright();
        FindDesiredDirection();
        FindAngularMomentum();
        FindTimeToStop();
        FindTourqeToUprightNow();
    }

    private void FixedUpdate()
    {
        CalculateVariables();

        DecideAppliedTorque();
        DecideIfUpright();

        if (!upright)
        {
            //Debug.Log(TimeUntillUpright() + " >= " + timeToStop);
            if (TimeUntillUpright() <= timeToStop) //not going to overshoot
            {
                rb.AddTorque(appliedTorque * desiredDirection);
            }
            else // allready going to fast
            {
                rb.AddTorque(appliedTorque * -desiredDirection);
            }
        }
        else if (rb.angularVelocity > 1)
        {
            // torque = mass * angularVelocityRadians / Time
            float tourqueToStop = angularMomentum / Time.fixedDeltaTime;
            rb.AddTorque(-tourqueToStop);
        }
    }
}
