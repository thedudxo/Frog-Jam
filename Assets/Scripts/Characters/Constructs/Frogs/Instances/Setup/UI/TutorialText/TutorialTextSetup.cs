using System.Collections.Generic;
using UnityEngine;
using Levels.UI;
using static ObjectInstanceBuilder;

namespace Frogs.Instances.Setups
{
    abstract class TutorialTextSetup : ISetup
    {
        readonly Frog frog;
        readonly ExtndGameObjSetup SetPlayerUILayer;

        public TutorialTextSetup(Frog frog)
        {
            this.frog = frog;
            SetPlayerUILayer = frog.setup.layers.SetObjectUILayer;
        }

        protected abstract void PlatformSpecificSetup(TutorialText text);

        public void Setup()
        {
            TutorialText tutorialText = GetTutorialText();

            PlatformSpecificSetup(tutorialText);
        }

        private TutorialText GetTutorialText()
        {
            //objectInstanceBuilder handles lists, but we only want one thing
            GameObject tutorialPrefab = frog.currentLevel.controllsTextTipPrefab.gameObject;
            var prefab = new List<GameObject> { tutorialPrefab };

            TutorialText instancedTutorialText = CreateInstances<TutorialText>
                (prefab, SetPlayerUILayer) [0];

            return instancedTutorialText;
        }
    }
}