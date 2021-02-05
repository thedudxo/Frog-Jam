using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;

namespace LevelScripts
{
    public class Split : MonoBehaviour
    {
        [Header("Manager - Don't forget to add this to the list on the manager")]
        [SerializeField] SplitManager splitManager;

        enum DetectionType { trigger, transform}
        [Header("params")]
        [SerializeField] DetectionType detectionType = DetectionType.trigger;
        [SerializeField] float DetectionXPos;

        [Header("GameObjects")]
        [SerializeField] Text title;
        [SerializeField] public Canvas playerCopyCanvas;

        public string Name { get; private set; }

        List<SplitEffect> effects = new List<SplitEffect>();

        private void Start()
        {
            playerCopyCanvas.gameObject.SetActive(false);
            Name = title.text;
        }

        public void AddSplitEffect(SplitEffect effect)
        {
            if (this.effects.Contains(effect))
            {
                Debug.Log(effect + "  allready exists in list", this);
                return;
            }
            this.effects.Add(effect);
        }

        private void Update()
        {
            if (detectionType != DetectionType.transform)
                return;

            foreach(SplitEffect effect in effects)
            {
                bool characterPastSplit = effect.CharacterTransform.position.x > DetectionXPos;

                if (characterPastSplit && !effect.triggeredThisLife)
                {
                    effect.ReachedSplit();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (detectionType != DetectionType.trigger)
                return;

            GameObject obj = collision.gameObject;
            bool isPlayer = obj.tag == GM.playerTag;
            int objInstanceID = obj.GetInstanceID();

            if (isPlayer)
            {
                foreach (SplitEffect effect in effects)
                {
                    bool collisionHasThisEffect = objInstanceID == effect.CharacterInstanceID;
                    if (collisionHasThisEffect)
                    {
                        effect.ReachedSplit();
                        break;
                    }
                }
            }
        }
    }
}
