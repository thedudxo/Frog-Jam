using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;

public class MockSphere : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;
    [SerializeField] Transform entryPoint;

    [SerializeField] bool hasEntered = false;

    public Runner runner;

    void Update()
    {
        transform.Translate(speed, 0, 0);

        if(hasEntered == false)
        {
            if (transform.position.x > entryPoint.position.x)
            {
                hasEntered = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
