using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Frogs.Instances;
using Levels;

public class DeathsAtLocationCounter : MonoBehaviour, INotifyBeforeDeath
{
    private int deaths = 0;
    [SerializeField] Text deathcounter;
    [SerializeField] Level level;

    void INotifyBeforeDeath.BeforeDeath()
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
            deathcounter.fontSize = 16;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var frog = level.frogManager.GetFrogComponent(collision.gameObject);
        if (frog == null) return;
        frog.events.UnsubscribeBeforeDeath(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var frog = level.frogManager.GetFrogComponent(collision.gameObject);
        if (frog == null) return;
        frog.events.SubscribeBeforeDeath(this);
    }

   
}
