using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 centerOffset;
    [SerializeField] float acceleration;
    public bool followPhill = true;

    float maxY = -1.85f; //camera wont go above this
    float ClosestWaveOffset = 4; // camera will move towards the wave as it approaches
    float waveDistanceAtClosetOffset = 8;

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
            //float targetX = 
            //float targetY = 

            //how much to move by
            float moveX = (transform.position.x - player.transform.position.x - centerOffset.x) ;
            float moveY = (transform.position.y - Mathf.Min(player.transform.position.y,maxY) - centerOffset.y) ;

            //move camera
            transform.position = new Vector3(
                (transform.position.x - moveX * acceleration),
                (transform.position.y - moveY * acceleration),
                transform.position.z);
        }
    }

}
