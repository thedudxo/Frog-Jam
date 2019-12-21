using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitManager : MonoBehaviour
{
    public decimal currentTime = 0;
    [SerializeField] Text timer;

    // Start is called before the first frame update
    void Start()
    {
        GM.splitManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += (decimal) Time.deltaTime;
        timer.text = decimal.Round(currentTime, 2) + "";
    }
}
