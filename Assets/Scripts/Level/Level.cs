using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] EditorSettings editorSettings;
        [SerializeField] GameObject endObject;

        [Header("Level Dimensions")]
        [SerializeField] public float end = 100;
        [SerializeField] public float startLength = 15;
        const float start = 0;

        [Header("Assigniees")]
        [SerializeField] public GameObject wave;

        private void OnValidate()
        {
            endObject.transform.position = new Vector3(end, 0, 0);
        }

        private void Awake()
        {
            GM.currentLevel = this;
        }

        private void OnDrawGizmos()
        {
            float gizmoYOffset = editorSettings.gizmoYOffset;
            float gizmoScale = editorSettings.gizmoScale;

            Vector3 startDraw = new Vector3(start, gizmoYOffset, 0);
            Vector3 endDraw = new Vector3(end, gizmoYOffset, 0);
            Vector3 spawnIndicatior = new Vector3(startLength, gizmoYOffset, 0);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(startDraw, endDraw);
            Gizmos.DrawSphere(startDraw, gizmoScale);
            Gizmos.DrawSphere(spawnIndicatior, gizmoScale);
            Gizmos.DrawSphere(endDraw, gizmoScale);
        }
    }
}
