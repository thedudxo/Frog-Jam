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
            float targetX = (player.transform.position.x + centerOffset.x);
            float targetY = (Mathf.Min(player.transform.position.y, maxY) + centerOffset.y);

            //wave weight


            //how much to move by
            float moveX = (targetX - transform.position.x) * acceleration;
            float moveY = (targetY - transform.position.y) * acceleration;

            //move camera
            transform.position = new Vector3(
                transform.position.x + moveX ,
                transform.position.y + moveY ,
                transform.position.z);
        }
    }

}
