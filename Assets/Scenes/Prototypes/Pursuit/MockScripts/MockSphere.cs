using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pursuits;

public class MockSphere : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;


    public Runner runner;

    void Update()
    {
        transform.Translate(speed, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
