using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimationTriggers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathAnimationFinished()
    {
        FrogManager.frogDeath.DeathAnimationFinished();
    }
}
