using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEachFrame : MonoBehaviour
{
    [SerializeField] Vector3 rotatePerFrame = new Vector3 (0,0,0);

    void Update()
    {
        transform.Rotate(rotatePerFrame);
    }
}
