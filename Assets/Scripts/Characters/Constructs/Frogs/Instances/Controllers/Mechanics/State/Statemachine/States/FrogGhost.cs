namespace Frogs.Instances.State
{
    public class FrogGhost : INotifyOnLeftPlatform
    {
        bool waitingToExitGhostMode = false;

        const int defaultLayer = 0;

        FrogCircleOverlap circleOverlap;
        Frog frog;
        FrogDeathConditions deathConditions;

        public FrogGhost(Frog frog, FrogDeathConditions deathConditions)
        {
            this.frog = frog;
            frog.events.SubscribeOnLeftPlatform(this);

            this.deathConditions = deathConditions;

            circleOverlap = new FrogCircleOverlap(frog);
            circleOverlap.radius = frog.collider.bounds.size.magnitude;
        }

        public void Activate()
        {
            frog.gameObject.layer = GM.NoSelfCollisionsLayer;
            frog.controllers.vfx.GhostVisuals();

            deathConditions.touchDeadly.Enabled = false;
        }

        public void Deactivate()
        {
            waitingToExitGhostMode = false;
            frog.gameObject.layer = defaultLayer;
            frog.controllers.vfx.UnGhostVisuals();
            deathConditions.touchDeadly.Enabled = true;
        }

        public void OnLeftPlatform() => waitingToExitGhostMode = true;

        public void Update()
        {
            if (waitingToExitGhostMode)
            {
                if (circleOverlap.IsOverlaping() == false)
                {
                    Deactivate();
                }
            }
        }
    }
}