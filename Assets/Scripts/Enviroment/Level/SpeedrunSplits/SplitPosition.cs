﻿using UnityEngine;
using Frogs.Instances;

namespace Levels
{
    public class SplitPosition : Split
    {
        [Header("params")]
        [SerializeField] float DetectionXPos;

        private void Update()
        {
            foreach (SplitEffect effect in effects)
            {
                bool characterPastSplit = effect.CharacterTransform.position.x > DetectionXPos;

                if (characterPastSplit && !effect.triggeredThisLife)
                {
                    effect.ReachedSplit();
                }
            }
        }
    }
}
