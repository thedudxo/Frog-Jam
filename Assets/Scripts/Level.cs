using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [Header("Level Dimensions")]
    public float startX = 0;
    public float endX = 100;
    [SerializeField] float spawnPlatformEndX;
    [HideInInspector] public float spawnPlatformLength;

    [Header("Debug UI")]
    [SerializeField] float gizmoYOffset = 0; //avoid drawing on the canvas boundry
    [SerializeField] float gizmoScale = 1;

    [Header("Assigniees")]
    public GameObject wave;
    [SerializeField] GameObject winScreen;



    private void Start()
    {
        GM.currentLevel = this;
        spawnPlatformLength = spawnPlatformEndX - startX;
    }

    private void Update()
    {
        GameObject frog = FrogManager.frog;

        // got to the end of the level (won game)
        if (frog.transform.position.x >= endX)
        {
            winScreen.SetActive(true);
            frog.SetActive(false);
        }

        //restart after winning
        if (winScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            winScreen.SetActive(false);
            frog.SetActive(true);
            FrogManager.frogDeath.KillPhill(true);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startDraw = new Vector3(startX, gizmoYOffset, 0);
        Vector3 endDraw = new Vector3(endX, gizmoYOffset, 0);
        Vector3 spawnIndicatior = new Vector3(spawnPlatformEndX, gizmoYOffset, 0);

        Gizmos.color = Color.red;

        Gizmos.DrawLine(startDraw, endDraw);
        Gizmos.DrawSphere(startDraw, gizmoScale);
        Gizmos.DrawSphere(spawnIndicatior, gizmoScale);
        Gizmos.DrawSphere(endDraw, gizmoScale);


    }
}
