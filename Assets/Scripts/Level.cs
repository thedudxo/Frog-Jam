using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{


    public float startX = 0;
    public float endX = 100;

    public GameObject player;
    public GameObject wave;


    float gizmoYOffset = 0; //avoid drawing on the canvas boundry
    float gizmoScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Vector3 startDraw = new Vector3(startX, gizmoYOffset, 0);
        Vector3 endDraw = new Vector3(endX, gizmoYOffset, 0);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(startDraw, endDraw);
        Gizmos.DrawSphere(startDraw, gizmoScale);
        Gizmos.DrawSphere(endDraw, gizmoScale);


    }
}
