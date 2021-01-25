using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    [SerializeField] List<Cloud> fluffyClouds = new List<Cloud>();
    [SerializeField] float averageSpeed;
    [SerializeField] float speedVariance;
    [SerializeField] float maxDistanceBehindCamera;
    [SerializeField] float spawnDistanceInFrontOfCamera;
    [SerializeField] float respawnDistanceVariance;
    [SerializeField] int desiredAmmountOfCloudsOnScreen = 3;
    [SerializeField] Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        //maxDistanceBehindCamera += FrogManager.frogDeath.respawnSetBack;

        foreach(Cloud cloud in fluffyClouds)
        {
            cloud.speed = Random.Range(averageSpeed - speedVariance, averageSpeed + speedVariance);
        }
    }

    private void Update()
    {
        return;

        int cloudsOnScreen = 0;
        foreach (Cloud cloud in fluffyClouds)
        {
            if (cloud.IsOnScreen())
            {
                cloudsOnScreen++;
            }
        }

        float cameraX = playerCamera.position.x;
        

        //work out if/where each cloud should be moved to
        foreach (Cloud cloud in fluffyClouds)
        {
            float cloudX = cloud.transform.position.x;

            //cloud is too far away:
            if (cloudX < cameraX - maxDistanceBehindCamera || //too far in behind OR
                cloudX > cameraX + spawnDistanceInFrontOfCamera + 2) // too far in front
            {

                //should the cloud be moved on or off screen?
                if (cloudsOnScreen < desiredAmmountOfCloudsOnScreen)
                {
                    //generate a new position in veiw of the camera, but not too far behind
                    float newXPosition = Random.Range(SingletonThatNeedsToBeRemoved.frog.transform.position.x, SingletonThatNeedsToBeRemoved.frog.transform.position.x + 20);

                    //move the cloud to its new spot, onscreen
                    cloud.transform.position = new Vector2(newXPosition, cloud.transform.position.y);
                    cloud.PopUp();
                    cloudsOnScreen++;
                }

                else
                {
                    //randomise its new position a bit
                    float spawnPosMeadian = cameraX + spawnDistanceInFrontOfCamera;
                    float newXPosition = Random.Range(spawnPosMeadian - respawnDistanceVariance, spawnPosMeadian + respawnDistanceVariance);

                    //move the cloud to its new spot, offscreen
                    cloud.transform.position = new Vector2(newXPosition, cloud.transform.position.y);
                    cloudsOnScreen--;
                }
            }
        }
    }

}
