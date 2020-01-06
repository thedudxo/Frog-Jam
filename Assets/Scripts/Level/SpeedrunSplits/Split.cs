using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class Split : MonoBehaviour
{
    [SerializeField] string splitName;
    [SerializeField] Text bestTimeText;
    [SerializeField] Text title;
    decimal bestTime;

    // Start is called before the first frame update
    void Start()
    {
        title.text = splitName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == GM.playerTag &&
            (GM.splitManager.currentTime < bestTime || bestTime == 0))
        {
            if(bestTime == 0 && GM.sendAnyalitics) {  //first time
                Analytics.CustomEvent("First Time at " + splitName, new Dictionary<string, object>
                { {"Time", GM.splitManager.currentTime}
                });
            }

            bestTime = GM.splitManager.currentTime;
            bestTimeText.text = decimal.Round(bestTime,2) + " sec";

            GM.splitManager.newPBParticles.gameObject.transform.position = FrogManager.frog.transform.position;
            GM.splitManager.newPBParticles.Emit(GM.splitManager.particleBurstCount);
        }
    }

    public string getSplitName()
    {
        return splitName;
    }

    public decimal getBestTime()
    {
        return bestTime;
    }
}
