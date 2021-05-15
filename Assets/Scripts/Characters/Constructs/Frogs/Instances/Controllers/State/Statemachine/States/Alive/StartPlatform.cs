namespace Frogs.Instances.State
{
    public class StartPlatform : INotifyOnRestart
    {
        readonly Frog frog;
        readonly float startPlatformEndPosition;
        readonly FrogDeathConditions deathConditions;
        public bool OnStartPlatform { get; private set; } = true;

        public StartPlatform(Frog frog, FrogDeathConditions deathConditions)
        {
            this.frog = frog;
            frog.events.SubscribeOnRestart(this);
            startPlatformEndPosition = frog.currentLevel.StartPlatformLength + frog.currentLevel.region.start;
            this.deathConditions = deathConditions;
        }

        public void Update()
        {
            if (OnStartPlatform) 
            { 
                if (frog.transform.position.x > startPlatformEndPosition)
                {
                    OnStartPlatform = false;
                    frog.events.SendOnLeftPlatform();
                    deathConditions.touchDeadly.Enabled = true;
                }
            }
        }

        public void OnRestart()
        {
            OnStartPlatform = true;
            deathConditions.touchDeadly.Enabled = false;
        }
    }
}