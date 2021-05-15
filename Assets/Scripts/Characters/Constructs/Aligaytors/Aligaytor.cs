using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aligaytor : MonoBehaviour
{
    [SerializeField] protected float maxAcceleration;
    [SerializeField] protected Rigidbody2D rb;
    protected new Transform transform;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        transform = rb.transform;
    }

}
