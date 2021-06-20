using UnityEngine;
using System.Collections.Generic;

namespace Frogs.Instances.Setups
{
    public class FrogSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogHudSetup Hud;
        [SerializeField] CameraSetup cameraSetup;
        [SerializeField] public FrogLayersSetup layers;

        KeybindsSetup keybindsSetup;
        TutorialTextSetup tutorialTextSetup;

        List<ISetup> setupModules;

        public void Setup(ViewMode viewMode)
        {
            frog.ViewMode = viewMode;
            var conditions = new Conditions { ViewMode = viewMode, Platform = GM.platform };


            CreateSetupModules();

            cameraSetup.Setup(viewMode);
            layers.Setup(viewMode);
            
            foreach(ISetup module in setupModules)
            {
                module.Setup(conditions);
            }
        }

        void CreateSetupModules()
        {
            setupModules = new List<ISetup>()
            {
                new KeybindsSetup(frog)
            };
        }
    }
}
