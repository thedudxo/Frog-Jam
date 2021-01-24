using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitManager : MonoBehaviour
{
    //manages all the split locations in a level

    public float currentTime = 0;
    [SerializeField] Text timer;
    public ParticleSystem newPBParticles;
    public int particleBurstCount = 20;
    public Split[] splits;

    // Start is called before the first frame update
    void Start()
    {
        GM.splitManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        timer.text = decimal.Round((decimal)currentTime, 2) + "";
    }
}
