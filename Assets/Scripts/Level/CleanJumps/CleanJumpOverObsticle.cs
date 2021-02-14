using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanJumpOverObsticle : MonoBehaviour
{ 

    [SerializeField] Text congratulationsText;
    [SerializeField] float displayTimeSeconds;
    [SerializeField] RememberCollisions[] rememberCollisions;
    float timer;
    bool timing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //remeber if the player has collided with these objects since dying
        if (collision.gameObject.tag == GM.playerTag)
        {

            bool collided = false;
            foreach(RememberCollisions remember in rememberCollisions)
            {
                if (remember.HasCollided)
                {
                    collided = true;
                }
            }

            if (!collided)
            {
                congratulationsText.enabled = true;
                timer = 0;
                timing = true;
            }
        }
    }

    private void Update()
    {
        if (!timing) return;

        timer += Time.deltaTime;
        if (timer >= displayTimeSeconds)
        {
            congratulationsText.enabled = false;
            timing = false;
        }
    }
}
