using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    readonly float defaultAcceleration = 0.05f;
    public float acceleration;

    public CameraTarget target;
    Vector2 targetPos;
    Vector2 centerOffset;
    Transform transform;

    float maxY = -1.85f;

    // TODO: camera will move towards the wave as it approaches
    //float ClosestWaveOffset = 4; 
    //float waveDistanceAtClosetOffset = 8;

    public CameraController(Transform cameraTransform, CameraTarget cameraTarget)
    {
        acceleration = defaultAcceleration;
        transform = cameraTransform;

        target = cameraTarget;

        if(target == null) target = new CameraTarget();
        Vector3 targetStart = target.GetPos();

        centerOffset = (transform.position - targetStart);
    }

    public void Update()
    {
       targetPos = target.GetPos();
    }

    //moving the camera in Update causes jitteryness
    public void MoveTowardsTarget()
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
