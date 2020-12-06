using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDynamicEffects : MonoBehaviour
{
    //things that happen in situations indirectly from player input, eg air particles / sound

    Rigidbody2D rb;


    [SerializeField] ParticleSystem airParticles;
    [SerializeField] GameObject debugCube;
    float airP_MaxEmmisonRate = 10;
    float AirEffects_MaxVelocity = 8f; // the velocity of the frogs rigidbody where particles will be max
    float AirEffects_MinVelocity = 4f; // velicities smaller than this wont have effetcs

    //modules the script will edit
    ParticleSystem.MainModule airP_main;
    ParticleSystem.EmissionModule airP_emision;
    ParticleSystem.ShapeModule airP_shape;

    private void Awake()
    {
        FrogManager.frogDynamicEffects = this;
    }

    void Start()
    {
        rb = FrogManager.frog.GetComponent<Rigidbody2D>();

        //get the modules needed
        airP_main = airParticles.main;
        airP_emision = airParticles.emission;
        airP_shape = airParticles.shape;
    }

    void Update()
    {


        //emit more particles if going faster
        float airEffects_Magnitude = Mathf.Clamp01((rb.velocity.magnitude - AirEffects_MinVelocity) / (AirEffects_MaxVelocity - AirEffects_MinVelocity));
        airP_emision.rateOverTime = airP_MaxEmmisonRate * airEffects_Magnitude;

        //make particles emit from the direction frog is traveling
        float angle = Mathf.Atan2(rb.velocity.x, -rb.velocity.y) * Mathf.Rad2Deg; //use sohcatoa to get the angle frog is traveling at
        airP_shape.rotation = new Vector3(0, 0, angle - 90);

        //make particles face the right way
        //airP_main.startRotation = angle +90;

    }
}
