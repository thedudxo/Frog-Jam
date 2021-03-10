using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;
using LevelScripts;

public class DeathsAtLocationCounter : MonoBehaviour, INotifyPreDeath
{
    private int deaths = 0;
    [SerializeField] Text deathcounter;
    [SerializeField] Level level;

    void INotifyPreDeath.PreDeath()
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
        var frog = level.frogManager.GetFrogComponent(collision.gameObject);
        if (frog == null) return;
        frog.events.UnsubscribePreDeath(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var frog = level.frogManager.GetFrogComponent(collision.gameObject);
        if (frog == null) return;
        frog.events.SubscribePreDeath(this);
    }

   
}
