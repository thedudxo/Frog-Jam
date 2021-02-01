﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;

namespace LevelScripts
{
    public interface ISplitEffect
    {
        void ReachedSplit();
        int TriggerInstanceID { get; set; }
    }

    public class Split : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] SplitManager splitManager;

        [Header("GameObjects")]
        [SerializeField] Text title;
        [SerializeField] public Canvas playerCopyCanvas;

        public string Name { get; private set; }

        List<ISplitEffect> effects = new List<ISplitEffect>();

        private void Start()
        {
            playerCopyCanvas.gameObject.SetActive(false);
            Name = title.text;
        }

        public void AddSplitEffect(ISplitEffect effect)
        {
            if (this.effects.Contains(effect))
            {
                Debug.Log(effect + "  allready exists in list", this);
                return;
            }
            this.effects.Add(effect);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject obj = collision.gameObject;
            bool isPlayer = obj.tag == GM.playerTag;

            if (isPlayer)
            {
                foreach (ISplitEffect effect in effects)
                {
                    bool collisionHasThisEffect = collision.gameObject.GetInstanceID() == effect.TriggerInstanceID;
                    if (collisionHasThisEffect)
                    {
                        effect.ReachedSplit();
                        break;
                    }
                }
            }
        }

        public bool IsPastSplit(float Xposition)
        {
            float splitXPos = transform.position.x;

            return Xposition > splitXPos;
        }

    }
}
