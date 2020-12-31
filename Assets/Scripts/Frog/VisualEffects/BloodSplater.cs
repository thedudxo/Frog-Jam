using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Frog.Vfx
{
    public class BloodSplater
    {
        Image splatter;
        const float startAlpha = .9f;
        const float decayAlpha = .03f;

        public BloodSplater(Image splatter)
        {
            this.splatter = splatter;
        }

        public void Update()
        {
            if (splatter.color.a >= 0)
            {
                Color colour = splatter.color;
                colour.a -= decayAlpha;
                splatter.color = colour;
            }
        }

        public void StartSplatter()
        {
            Color colour = splatter.color;
            colour.a = startAlpha;
            splatter.color = colour;
        }
    }
}
