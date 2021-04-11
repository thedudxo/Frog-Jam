using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;
using UnityEngine.Serialization;

namespace LevelScripts
{
    public abstract class Split : MonoBehaviour
    {
        [SerializeField] Level level;
        [SerializeField] Text title;
        [SerializeField] [FormerlySerializedAs("playerCopyCanvas")] public Canvas canvasPrototype;

        public string SplitName { get; private set; }

        protected List<SplitEffect> effects = new List<SplitEffect>();

        protected virtual void Start()
        {
            level.splitManager.AddSplit(this);
            canvasPrototype.gameObject.SetActive(false);
            SplitName = title.text;
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
    }
}
