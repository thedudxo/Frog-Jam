using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if(collision.gameObject.tag == GM.playerTag &&
            (GM.splitManager.currentTime < bestTime || bestTime == 0))
        {
            bestTime = GM.splitManager.currentTime;
            bestTimeText.text = decimal.Round(bestTime,2) + " sec";
        }
    }
}
