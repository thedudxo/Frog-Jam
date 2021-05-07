using UnityEngine;
using UnityEngine.UI;


namespace Frogs.Vfx
{
    public class ImageFadeout
    {
        Image img;
        const float startAlpha = .9f;
        const float decayAlpha = .03f;

        public ImageFadeout(Image splatter)
        {
            this.img = splatter;
        }

        public void Update()
        {
            if (img.color.a >= 0)
            {
                Color colour = img.color;
                colour.a -= decayAlpha;
                img.color = colour;
            }
        }

        public void StartFade()
        {
            Color colour = img.color;
            colour.a = startAlpha;
            img.color = colour;
        }
    }
}
