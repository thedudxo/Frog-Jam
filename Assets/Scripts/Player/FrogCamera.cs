using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCamera : MonoBehaviour
{
    [SerializeField] Camera frogCamera;
    [SerializeField] Vector3 offset;
    [SerializeField] float cameraLag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frogCamera.gameObject.transform.position = transform.position + offset;
    }
}
