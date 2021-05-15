namespace Frogs.Instances.State
{
    public class FrogGhost : INotifyOnLeftPlatform
    {
        public bool IsActive { get; private set; } = false;

        bool waitingToExitGhostMode = false;

        const int defaultLayer = 0;

        FrogCircleOverlap circleOverlap;
        Frog frog;

        public FrogGhost(Frog frog)
        {
            this.frog = frog;
            frog.events.SubscribeOnLeftPlatform(this);

            circleOverlap = new FrogCircleOverlap(frog);
            circleOverlap.radius = frog.collider.bounds.size.magnitude;
        }

        public void Activate()
        {
            IsActive = true;

            frog.gameObject.layer = GM.NoSelfCollisionsLayer;
            frog.controllers.vfx.GhostVisuals();
        }

        public void Deactivate()
        {
            IsActive = false;

            waitingToExitGhostMode = false;
            frog.gameObject.layer = defaultLayer;
            frog.controllers.vfx.UnGhostVisuals();
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