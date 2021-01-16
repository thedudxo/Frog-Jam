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

    /* 
     * 
     * 
     * 
     * 
     * 
     * 
     */ 


    private void OnCollisionEnter2D(Collision2D collision)
    {

        // use up some velocity
        Vector2 velocity = collision.relativeVelocity;

        Vector2 usedVelocity = velocity * squishResistanceNrm;
        Vector2 remainingVelocity = velocity - usedVelocity;

        //f = ma    =    force = mass * (velocity / time)
        Vector2 Force = rb.mass * (remainingVelocity / Time.fixedDeltaTime);
        rb.AddForce(Force);

        //convert it to squish
        float usedSpeedNrm = Mathf.Clamp01(usedVelocity.magnitude / maxSquishSpeed);

        //RaycastHit2D ray = Physics2D.Raycast(rb.transform.position,)

        //sohcatoa
        float angle = Mathf.Atan2(velocity.x, -velocity.y) * Mathf.Rad2Deg;
        squisher.transform.rotation = Quaternion.Euler(0,0, angle);
    }
}
