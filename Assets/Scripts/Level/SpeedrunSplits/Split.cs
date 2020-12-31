using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class Split : MonoBehaviour, IRespawnResetable
{
    [SerializeField] string splitName;
    [SerializeField] Text bestTimeText;
    [SerializeField] Text title;
    decimal bestTime;

    bool triggeredThisLife = false;

    // Start is called before the first frame update
    void Start()
    {
        title.text = splitName;
        GM.AddRespawnResetable(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GM.playerTag && ! triggeredThisLife)
        {

            triggeredThisLife = true;

            if (GM.splitManager.currentTime < bestTime || bestTime == 0)
            {
                if (bestTime == 0 && GM.sendAnyalitics) {  //first time
                    Analytics.CustomEvent("First Time at " + splitName, new Dictionary<string, object>
                { {"Time", GM.splitManager.currentTime}
                });
                }

                bestTime = GM.splitManager.currentTime;
                bestTimeText.text = decimal.Round(bestTime, 2) + " sec";
            }


            //particles
            ParticleSystem SplitParticles = GM.splitManager.newPBParticles;
            SplitParticles.gameObject.transform.position = new Vector3(
                SingletonThatNeedsToBeRemoved.frog.transform.position.x, SingletonThatNeedsToBeRemoved.frog.transform.position.y, SplitParticles.gameObject.transform.position.z);
            SplitParticles.Emit(GM.splitManager.particleBurstCount);
        }
    }

    public void PhillRespawned()
    {
        triggeredThisLife = false;
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
