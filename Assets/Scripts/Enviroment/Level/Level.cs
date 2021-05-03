using System.Collections.Generic;
using UnityEngine;
using Frogs.Collections;
using Waves;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] EditorSettings editorSettings;
        [SerializeField] GameObject objective;

        [Header("Level Dimensions")]
        [SerializeField] public Region region;
        [SerializeField] public float StartPlatformLength { get; private set; } = 15;

        [Header("Components")]
        [SerializeField] public SplitManager splitManager;
        [SerializeField] public FrogCollection frogManager;
        [SerializeField] public WaveCollection waveManager;
        [SerializeField] public UI.ControllsTextTip controllsTextTipPrefab;

        [HideInInspector] public List<CleanlyJumpableObstacle> cleanJumps;


        private void OnValidate()
        {
            if (objective == null) return;
            objective.transform.position = new Vector3(region.end, objective.transform.position.y, objective.transform.position.z);
        }

        private void Awake()
        {
            GM.currentLevel = this;
            Cursor.visible = false;
        }

        private void OnDrawGizmos()
        {
            float gizmoYOffset = editorSettings.gizmoYOffset;
            float gizmoScale = editorSettings.gizmoScale;

            Vector3 startDraw = new Vector3(region.start, gizmoYOffset, 0);
            Vector3 endDraw = new Vector3(region.end, gizmoYOffset, 0);
            Vector3 spawnIndicatior = new Vector3(StartPlatformLength, gizmoYOffset, 0);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(startDraw, endDraw);
            Gizmos.DrawSphere(startDraw, gizmoScale);
            Gizmos.DrawSphere(spawnIndicatior, gizmoScale);
            Gizmos.DrawSphere(endDraw, gizmoScale);
        }
    }
}
