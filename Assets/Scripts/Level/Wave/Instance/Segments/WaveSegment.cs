using System.Collections;
using UnityEngine;

namespace WaveScripts
{
    public class WaveSegment : MonoBehaviour
    {
        [SerializeField] protected Wave wave;
        [SerializeField] Animator animator;

        bool hidden = false;

        bool segmentAtBreakpoint => transform.position.x > wave.breakPosition;

        public void HideSegmentAtBreakpoint()
        {
            if (!hidden && segmentAtBreakpoint)
            {
                HideSegment();
            }
        }
        protected virtual void HideSegment()
        {
            animator.SetTrigger("Disappear");
            hidden = true;
        }

        public void UnHideSegement()
        {
            animator.SetTrigger("PopUp");
            hidden = false;
        }
    }
}
