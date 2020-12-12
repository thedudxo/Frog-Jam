using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCamera : MonoBehaviour
{
    readonly float defaultAcceleration = 0.05f;
    public float acceleration;
    public CameraTarget target;

    Vector2 targetPos;
    Vector2 centerOffset;

    float maxY = -1.85f; //camera wont go above this
    float ClosestWaveOffset = 4; // camera will move towards the wave as it approaches
    float waveDistanceAtClosetOffset = 8;

    private void Awake()
    {
        FrogManager.frogCamera = this;
    }

    void Start()
    {
        acceleration = defaultAcceleration;

        if(target == null) target = new CameraTarget();
        Vector3 targetStart = target.GetPos();

        centerOffset = (transform.position - targetStart);
    }

    private void Update()
    {
       targetPos = target.GetPos();
    }

    //moving the camera in Update causes jitteryness
    private void FixedUpdate()
    {

        float offsetTargetX = (targetPos.x + centerOffset.x);
        float offsetTargetY = (Mathf.Min(targetPos.y, maxY) + centerOffset.y);


        float moveX = (offsetTargetX - transform.position.x) * acceleration;
        float moveY = (offsetTargetY - transform.position.y) * acceleration;


        transform.position = new Vector3(
            transform.position.x + moveX ,
            transform.position.y + moveY ,
            transform.position.z);
    }

}
