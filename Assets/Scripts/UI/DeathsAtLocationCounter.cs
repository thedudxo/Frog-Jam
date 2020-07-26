using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathsAtLocationCounter : MonoBehaviour
{
    private int deaths = 0;
    [SerializeField] TextMesh deathcounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GM.playerTag)
        {
            deaths++;
            Statistics.deathsAtThatHole++; //TODO: make more dynamic for future uses in other levels
            deathcounter.text = ""+ deaths;
            if(deaths > 99)
            {
                deathcounter.text = "heaps";
                deathcounter.fontSize = 60;
            }
        }
    }
}
