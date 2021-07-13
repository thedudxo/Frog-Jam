using UnityEngine;
using UnityEngine.UI;

namespace Frogs.Instances.Jumps
{
    public class JumpPowerBar
    {
        Slider powerBar;

        public JumpPowerBar(Slider powerBar)
        {
            if (powerBar == null) throw new System.ArgumentNullException("powerBar");
            this.powerBar = powerBar;

            powerBar.minValue = 0;
            powerBar.maxValue = 1;
        }

        public void SetValue(float value)
        {
            powerBar.value = Mathf.Clamp01(value);
        }
    }
}
