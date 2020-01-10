using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowColliders : MonoBehaviour
{

    void OnDrawGizmos()
    {
       foreach (Collider collider in GetComponents<Collider>())
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
        
    }
}
