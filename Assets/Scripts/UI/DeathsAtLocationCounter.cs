using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathsAtLocationCounter : MonoBehaviour, IDeathResetable
{
    private int deaths = 0;
    [SerializeField] Text deathcounter;

    bool playerAtDeathCounterLocation = false;



    void IDeathResetable.PhillDied()
    {
        deaths++;
        Statistics.deathsAtThatHole++; //TODO: make more dynamic for future uses in other levels
        deathcounter.text = "" + deaths;
        if (deaths > 99)
        {
            deathcounter.text = "heaps";
            deathcounter.fontSize = 60;
        }
    }

    IEnumerator TriggerExit()
    {
        yield return null;

        GM.RemoveDeathResetable(this);
        playerAtDeathCounterLocation = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GM.playerTag)
        {
            //delay by a frame because execution order will cause this to run meaning PhillDied() never gets called
            StartCoroutine("TriggerExit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GM.playerTag)
        {

            GM.AddDeathResetable(this);
            playerAtDeathCounterLocation = true;
        }
    }

   
}
