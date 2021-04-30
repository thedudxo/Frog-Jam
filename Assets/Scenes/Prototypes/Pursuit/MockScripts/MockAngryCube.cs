using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;

public class MockAngryCube : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;

    public Pursuer pursuer;

    void Update()
    {
        transform.Translate(speed, 0, 0);

    }
}
