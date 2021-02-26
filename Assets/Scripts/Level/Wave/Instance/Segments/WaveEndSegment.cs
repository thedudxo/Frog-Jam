using UnityEngine;

namespace waveScripts
{
    public class WaveEndSegment : WaveSegment
    {
        float WaitTillRestart = 2;
        float waited = 0;
        bool waiting = false;

        void Update()
        {
            if (waiting)
            {
                waited += Time.deltaTime;
                if (waited > WaitTillRestart)
                {
                    waiting = false;
                    waited = 0;
                    wave.state = Wave.State.inactive;
                }
            }
        }

        protected override void HideSegment()
        {
            base.HideSegment();
            waiting = true;
        }
    }
}