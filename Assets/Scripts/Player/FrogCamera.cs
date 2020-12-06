using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 centerOffset;
    [SerializeField] float acceleration;
    public bool followPhill = true;

    //float YDampening = 0.5f; // how much to follow in the Y axis, 0 being none, 1 being fully
    float maxY = -1.85f; //camera wont go above this

    private void Awake()
    {
        FrogManager.frogCamera = this;
    }

    void Start()
    {
        centerOffset = transform.position - player.transform.position;
    }

    void FixedUpdate()
    {
        if (followPhill)
        {
            //how much to move by
            float moveX = (transform.position.x - player.transform.position.x - centerOffset.x) ;
            float moveY = (transform.position.y - Mathf.Min(player.transform.position.y,maxY) - centerOffset.y) ;
            //float moveY = transform.position.y ;

            //move camera
            transform.position = new Vector3(
                (transform.position.x - moveX * acceleration),
                (transform.position.y - moveY * acceleration),
                transform.position.z);
        }
    }

}
