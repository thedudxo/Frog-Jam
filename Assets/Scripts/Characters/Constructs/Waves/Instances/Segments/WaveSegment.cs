using System.Collections;
using UnityEngine;

namespace Waves
{
    public class WaveSegment : MonoBehaviour
    {
        [SerializeField] protected Wave wave;
        [SerializeField] Animator animator;

        bool hidden = false;

        bool SegmentAtBreakpoint => transform.position.x > wave.breakControlls.BreakPosition;

        public void HideSegmentAtBreakpoint()
        {
            if (!hidden && SegmentAtBreakpoint)
            {
                HideSegment();
            }
        }
        protected virtual void HideSegment()
        {
            Debug.Log("hiding");
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
