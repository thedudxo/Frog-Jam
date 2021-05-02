using UnityEngine;

namespace WaveScripts
{
    public class WaveEndSegment : WaveSegment
    {
        float WaitTillRestart = 2;
        float waited = 0;
        bool waitForAnimation = false;

        void Update()
        {
            if (waitForAnimation)
            {
                waited += Time.deltaTime;
                if (waited > WaitTillRestart)
                {
                    waitForAnimation = false;
                    waited = 0;
                    wave.breakControlls.StopBreaking();
                }
            }
        }

        protected override void HideSegment()
        {
            base.HideSegment();
            waitForAnimation = true;
        }
    }
}