using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockAngryCube : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;
    [SerializeField] Transform entryPoint;

    [SerializeField] bool hasEntered = false;

    void Update()
    {
        transform.Translate(speed, 0, 0);

        if (hasEntered == false)
        {
            if (transform.position.x > entryPoint.position.x)
            {
                hasEntered = true;
            }
        }
    }
}
