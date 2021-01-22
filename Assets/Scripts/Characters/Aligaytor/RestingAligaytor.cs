using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhysicsUtil1D;

public class RestingAligaytor : Aligaytor
{
    float restPos;
    const float restPosTolerance = 0.01f;
    const float maxSpeed = 10;

    private void Start()
    {
        restPos = rb.position.x;

        //testing
        restPos = rb.position.x - 10;
        //rb.AddForce(new Vector2(50, 0));
    }

    bool AtPos => AtPosition(transform.position.x, restPos, restPosTolerance);
    int Direction => DirectionToPosition(transform.position.x, restPos);
    bool Overshooting => WillOvershoot(rb.position.x, restPos, rb.velocity.x, maxAcceleration);

    private void FixedUpdate()
    {
        //testing
        //rb.AddForce(new Vector2(-2, 0));

        if (AtPos)
        {
            Debug.Log("atPos");
            if(rb.velocity.x != 0) SlowDown();
        }
        else
        {
            if (Overshooting) SlowDown();
            else Accelerate();
        }
    }

    void SlowDown()
    {
        //Debug.Log("slowdown");
        float forceToStop = Mathf.Abs(ForceToStop(rb.mass, rb.velocity.x));
        float force = Mathf.Min(forceToStop, maxAcceleration);

        force = force * -Direction(rb.velocity.x);

        Debug.Log(force);
        rb.AddForce(new Vector2(force, 0));
    }

    void Accelerate()
    {
        //Debug.Log("accelerate");
        Vector2 force = new Vector2(maxAcceleration * Direction, 0);
        rb.AddForce(force);
    }



    /* 
     * compare f to maxF, pick smallest
     * f=force to stop right now, t = time.fixedDeltaTime
     * f=ma
     * f=m(v/t)
     * f=m((d/t)/t)
     * f=m(d/t^2)
     * 
     * maxF = m*aligaytor.acceleration
     */

}
