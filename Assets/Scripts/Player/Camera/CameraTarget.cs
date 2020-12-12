﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget
{ 
    Transform targetTransform;
    Vector2 targetVector;

    public enum TargetType{ transform, vector2, none}
    TargetType targetType;

    public CameraTarget()
    {
        targetType = TargetType.none;
        Debug.LogWarning("CameraTarget not assigned");
    }

    public CameraTarget(Transform t)
    {
        SetTarget(t);
    }

    public CameraTarget(Vector3 v)
    {
        SetTarget(v);
    }

    public CameraTarget(Vector2 v)
    {
        SetTarget(v);
    }



    public void SetTarget(Transform t)
    {
        targetType = TargetType.transform;
        targetTransform = t;
        if (targetVector == null) targetVector = new Vector2();
    }

    public void SetTarget(Vector3 pos)
    {
        SetTarget(new Vector2(pos.x, pos.y));
    }

    public void SetTarget(Vector2 pos)
    {
        targetType = TargetType.vector2;
        targetVector = pos;
    }

    public void SetTarget(float xPos, float YPos)
    {
        targetType = TargetType.vector2;
        if (targetVector == null) targetVector = new Vector2();
        targetVector.x = xPos;
        targetVector.y = YPos;
    }


    public Vector2 GetPos()
    {
        switch (targetType)
        {
            case TargetType.transform:
                targetVector.x = targetTransform.position.x;
                targetVector.y = targetTransform.position.y;
                return targetVector;

            case TargetType.vector2:
                return targetVector;
        }

        return Vector2.zero;
    }

}
