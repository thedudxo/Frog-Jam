using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;

public class DeathsAtLocationCounter : MonoBehaviour, INotifyOnDeath
{
    private int deaths = 0;
    [SerializeField] Text deathcounter;

    void INotifyOnDeath.OnDeath()
    {
        deaths++;
        Statistics.deathsAtThatHole++; //TODO: make more dynamic for future uses in other levels
        UpdateUI();
    }

    private void UpdateUI()
    {
        deathcounter.text = "" + deaths;
        if (deaths > 99)
        {
            deathcounter.text = "heaps";
            deathcounter.fontSize = 60;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GM.playerTag)
        {
            collision.GetComponent<Frog>().UnscubscribeOnDeath(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GM.playerTag))
        {
            collision.GetComponent<Frog>().SubscribeOnDeath(this);
        }
    }

   
}
