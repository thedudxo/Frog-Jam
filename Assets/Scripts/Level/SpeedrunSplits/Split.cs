using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrogScripts;

namespace LevelScripts
{
    public abstract class Split : MonoBehaviour
    {
        [Header("Don't forget to add this to the list on the SplitManager")]

        [SerializeField] Text title;
        [SerializeField] public Canvas playerCopyCanvas;

        public string SplitName { get; private set; }

        protected List<SplitEffect> effects = new List<SplitEffect>();

        protected virtual void Start()
        {
            playerCopyCanvas.gameObject.SetActive(false);
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
