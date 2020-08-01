using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour, IRespawnResetable
{

    [SerializeField] List<Cloud> fluffyClouds = new List<Cloud>();
    [SerializeField] float averageSpeed;
    [SerializeField] float speedVariance;
    [SerializeField] float maxDistanceBehindCamera;
    [SerializeField] float spawnDistanceInFrontOfCamera;

    // Start is called before the first frame update
    void Start()
    {
        maxDistanceBehindCamera += FrogManager.frogDeath.respawnSetBack;

        GM.AddRespawnResetable(this);

        foreach(Cloud cloud in fluffyClouds)
        {
            cloud.speed = Random.Range(averageSpeed - speedVariance, averageSpeed + speedVariance);
        }
    }

    public void RespawnReset()
    {
        foreach (Cloud cloud in fluffyClouds)
        {
            cloud.transform.position = new Vector2(
                cloud.transform.position.x - FrogManager.frogDeath.respawnSetBack,
                cloud.transform.position.y
                );
        }
            
    }

    private void Update()
    {
        foreach (Cloud cloud in fluffyClouds)
        {
            //cloud moved too far behind camera, so pull it forward.
            if (cloud.transform.position.x < FrogManager.frogCamera.transform.position.x - maxDistanceBehindCamera)
            {
                cloud.transform.position = new Vector2(
                    FrogManager.frogCamera.transform.position.x + spawnDistanceInFrontOfCamera,
                    cloud.transform.position.y
                    );
            }
        }
    }

}
