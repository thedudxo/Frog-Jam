using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishyObject : MonoBehaviour
{
    [SerializeField] GameObject squisher;
    [SerializeField] GameObject squishiee;
    [SerializeField] Rigidbody2D rb;


    const float maxSquishSpeed = 20;
    const float squishRange = 0.3f;
    const float squishDefault = 1.0f ;
    const float squishResistanceNrm = 0.5f;

    const float uprightAngleRads = 0;
    const float maxTourqe = 5f;

    float currentRotation;
    float radsToUpright;

    private void Start()
    {
        //accelerate to 0* untill need to slow down to stop at 0*

        //find time required to stop with max tourqe
        //accelerate untill not enough time left
        //start slowing down in time to stop at 0

        // torque = force * lever length 
        // torque = force * 1 https://docs.unity3d.com/ScriptReference/Rigidbody2D.AddTorque.html
        // torque = force
        // torque = m(r^2)α https://courses.lumenlearning.com/boundless-physics/chapter/dynamics/
        // torque = mass * (1^2) * αngularAcceleration
        // torque = mass * αngularAcceleration https://courses.lumenlearning.com/physics/chapter/10-1-angular-acceleration/
        // torque = mass * angularVelocityRadians / Time
        // time = ( mass * angularVelocityRadians ) / torque

        float angularVelocity = rb.angularVelocity * Mathf.Deg2Rad;

        // torque / mass = αngularAcceleration
        float maxAngularAcceleration = maxTourqe / rb.mass;


        //rb.AddTorque(-10);
    }

    private float TimeUntillUpright()
    {
        //v  = d/t      vt = d      t  = d/v
        float angularVelocityRads = rb.angularVelocity * Mathf.Deg2Rad;

        bool radsToUprightPositive = radsToUpright >= 0;
        bool angularVelocityRadsPositive = angularVelocityRads >= 0;

        float time = radsToUpright / angularVelocityRads;


        bool goingTowardsUpright = !( (radsToUpright >= 0) ^ (angularVelocityRads >= 0) );

        if (goingTowardsUpright)
        {
            
            return time;
        }
        else
        {
            float timeForFullRotation = 2 * Mathf.PI / Mathf.Abs(angularVelocityRads);
            return timeForFullRotation - Mathf.Abs(time);
        }
    }

    private void FixedUpdate()
    {
        currentRotation = rb.transform.rotation.eulerAngles.z % 360;

        if(currentRotation < 180) radsToUpright = uprightAngleRads - (currentRotation * Mathf.Deg2Rad);
        else radsToUpright = uprightAngleRads - (-(180-(currentRotation % 180)) * Mathf.Deg2Rad);


        float desiredDirection = -1; if (currentRotation < 180) { desiredDirection = 1; }

        float angularMomentum = rb.mass * rb.angularVelocity * Mathf.Deg2Rad;

        // time = ( mass * angularVelocityRadians ) / torque
        float timeToStop = Mathf.Abs(angularMomentum / maxTourqe);

        // torque = mass * angularVelocityRadians / Time 
        // torque = mass * (distance/(time^2))
        float torqueToUprightNow = Mathf.Abs(rb.mass * (radsToUpright / Mathf.Pow(Time.fixedDeltaTime, 2)));


        float appliedTorque;
        if (torqueToUprightNow > maxTourqe) appliedTorque = maxTourqe;
        else appliedTorque = torqueToUprightNow;

        const float tolerance = 0.1f;
        bool notUpright = currentRotation * Mathf.Deg2Rad > uprightAngleRads + tolerance || currentRotation * Mathf.Deg2Rad < uprightAngleRads - tolerance;

        if (notUpright)
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


    private void OnCollisionEnter2D(Collision2D collision)
    {

        // use up some velocity
        Vector2 velocity = collision.relativeVelocity;

        Vector2 usedVelocity = velocity * squishResistanceNrm;
        Vector2 remainingVelocity = velocity - usedVelocity;

        //f = ma    =    force = mass * (velocity / time)
        Vector2 Force = rb.mass * (remainingVelocity / Time.fixedDeltaTime);
        rb.AddForce(-Force);

        //convert it to squish
        float usedSpeedNrm = Mathf.Clamp01(usedVelocity.magnitude / maxSquishSpeed);

        //RaycastHit2D ray = Physics2D.Raycast(rb.transform.position,)

        //sohcatoa
        float angle = Mathf.Atan2(velocity.x, -velocity.y) * Mathf.Rad2Deg;
        squisher.transform.rotation = Quaternion.Euler(0,0, angle);
    }
}
