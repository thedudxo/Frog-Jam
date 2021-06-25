using UnityEngine;
using System.Collections.Generic;

namespace Frogs.Instances.Setups
{
    public class FrogSetup : MonoBehaviour
    {
        [SerializeField] Frog frog;
        [SerializeField] FrogHudSetup Hud;
        [SerializeField] public FrogLayersSetup layers;

        List<ISetup> setupModules;

        public void Setup(ViewMode viewMode)
        {
            frog.ViewMode = viewMode;
            var conditions = new Conditions { 
                ViewMode = viewMode,
                Platform = GM.platform };

            CreateSetupModules();
            
            foreach(ISetup module in setupModules)
            {
                module.Setup(conditions);
            }
        }

        void CreateSetupModules()
        {
            setupModules = new List<ISetup>()
            {
                new CameraSetup(frog),
                new KeybindsSetup(frog),
                layers,
                new TutorialTextSetup(frog),
                Hud
            };
        }
    }
}
