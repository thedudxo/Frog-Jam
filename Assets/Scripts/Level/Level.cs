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
        [SerializeField] GameObject winScreen;
        [SerializeField] Frog.Frog frog;

        //stats
        Timer timer = new Timer();
        int deaths;
        int? pbDeaths;

        /*level needs to:
         *players should:
            track their own time + deaths
        */

        private void OnValidate()
        {
            endObject.transform.position = new Vector3(end, 0, 0);
        }

        private void Awake()
        {
            GM.currentLevel = this;
        }

        bool PlayerGotToTheEnd => frog.transform.position.x >= end;
        bool PlayerInputRestart => Input.GetKeyDown(KeyCode.Q);
        private void Update()
        {
            switch (GM.gameState)
            {
                case GM.GameState.playingLevel:
                    timer.Update();
                    if (PlayerGotToTheEnd) FinishLevel();
                    break;

                case GM.GameState.finishedLevel:
                    if (PlayerInputRestart) RestartLevel();
                    break;
            }
        }

        private void RestartLevel()
        {
            timer.Reset();
            GM.levelEndScreen.Disable();
            GM.gameState = GM.GameState.playingLevel;
            frog.RestartLevel();
        }

        private void FinishLevel()
        {
            timer.CheckForNewPB();
            GM.gameState = GM.GameState.finishedLevel;
            EnableEndScreen();
            FrogManager.frog.GetComponent<Rigidbody2D>().gravityScale = 0;
            FrogManager.frog.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        private void EnableEndScreen()
        {
            pbDeaths = Mathf.Min(pbDeaths ?? int.MaxValue, deaths);
            GM.levelEndScreen.Enable(timer.Time, (float)timer.PbTime, deaths, (int)pbDeaths);
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
