using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowColliders2D : MonoBehaviour
{

    void OnDrawGizmos()
    {
       foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
        
    }
}
