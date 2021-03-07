using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] EditorSettings editorSettings;
        [SerializeField] GameObject objective;

        [Header("Level Dimensions")]
        [SerializeField] public float end = 100;
        [SerializeField] public float startLength = 15;
        const float start = 0;

        [Header("Components")]
        [SerializeField] public GameObject wave;
        [SerializeField] public SplitManager splitManager;
        [SerializeField] public FrogManager frogManager;
        [SerializeField] public waveScripts.WaveManager waveManager;
        [HideInInspector] public List<CleanlyJumpableObstacle> cleanJumps;
        [SerializeField] public WaveFrogMediatior waveFrogMediatior;

        private void OnValidate()
        {
            if (objective == null) return;
            objective.transform.position = new Vector3(end, objective.transform.position.y, objective.transform.position.z);
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
