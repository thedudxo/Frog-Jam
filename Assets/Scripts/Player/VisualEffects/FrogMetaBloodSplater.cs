using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogMetaBloodSplater : MonoBehaviour
{
    [SerializeField] Image splatter;
    [SerializeField] float startAlpha;
    [SerializeField] float decayAlpha;


    void Update()
    {
        if(splatter.color.a >= 0)
        {
            var colour = splatter.color;
            colour.a -= decayAlpha;
            splatter.color = colour;
        }
    }

    public void startSplatter()
    {
        var colour = splatter.color;
        colour.a = startAlpha;
        splatter.color = colour;
    }
}
