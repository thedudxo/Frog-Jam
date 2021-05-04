﻿
using System.Collections.Generic;
using UnityEngine;

namespace Waves
{
    public class WaveSegmentManager : MonoBehaviour
    {
        [SerializeField] Wave wave;
        public const string segmentTag = "WaveSegment";
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

        public void HideAtBreakpoint()
        {
            foreach (WaveSegment segment in segments)
            {
                segment.HideSegmentAtBreakpoint();
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
