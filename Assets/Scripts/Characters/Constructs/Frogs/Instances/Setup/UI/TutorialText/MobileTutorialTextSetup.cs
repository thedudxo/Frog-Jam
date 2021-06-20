using Levels.UI;

namespace Frogs.Instances.Setups
{
    class MobileTutorialTextSetup : TutorialTextSetup
    {
        public MobileTutorialTextSetup(Frog frog) : base(frog) { }

        protected override void PlatformSpecificSetup(TutorialText text) 
        {
            text.SetMobile();
        }
    }
}