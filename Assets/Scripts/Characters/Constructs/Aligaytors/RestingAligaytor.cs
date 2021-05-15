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
    }

    bool AtPos => AtPosition(transform.position.x, restPos, restPosTolerance);
    int Direction => DirectionToPosition(transform.position.x, restPos);
    bool Overshooting => WillOvershoot(rb.position.x, restPos, rb.velocity.x, maxAcceleration);

    private void FixedUpdate()
    {
        if (AtPos)
        {
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
        float forceToStop = Mathf.Abs(ForceToStop(rb.mass, rb.velocity.x));
        float force = Mathf.Min(forceToStop, maxAcceleration);

        force = force * -Direction(rb.velocity.x);

        rb.AddForce(new Vector2(force, 0));
    }

    void Accelerate()
    {
        Vector2 force = new Vector2(maxAcceleration * Direction, 0);
        rb.AddForce(force);
    }
}
