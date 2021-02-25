using UnityEngine;
using System.Collections.Generic;

namespace waveScripts
{
    public class WaveSegmentManager : MonoBehaviour
    {
        [SerializeField] Wave wave;
        const string segmentTag = "WaveSegment";
        [SerializeField] public List<WaveSegment> segments = new List<WaveSegment>();

        private void Start()
        {
            GetSegmentsFromComponents();

            void GetSegmentsFromComponents()
            {
                foreach (Transform t in wave.transform)
                {
                    if (t.CompareTag(segmentTag))
                    {
                        WaveSegment seg = t.GetComponent<WaveSegment>();
                        if (seg == null) Debug.LogError($"Gameobject was tagged with '{segmentTag}' but did not contain a segment component", t);
                        segments.Add(seg);
                    }
                }
            }
        }

        public void CheckSegments()
        {
            foreach (WaveSegment segment in segments)
            {
                segment.CheckSegment();
            }
        }

        public void UnHideSegments()
        {
            foreach (WaveSegment segment in segments)
            {
                segment.UnHideSegement();
            }
        }
    }
}
