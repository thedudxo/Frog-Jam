using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 centerOffset;
    [SerializeField] float acceleration;

    // Start is called before the first frame update
    void Start()
    {
        centerOffset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //how much to move by
        float moveX = (transform.position.x - player.transform.position.x - centerOffset.x) * acceleration;
        float moveY = (transform.position.y - player.transform.position.y - centerOffset.y) * acceleration;

        //move camera
        transform.position = new Vector3(
            (transform.position.x - moveX),
            (transform.position.y - moveY),
            transform.position.z);


    }
}
