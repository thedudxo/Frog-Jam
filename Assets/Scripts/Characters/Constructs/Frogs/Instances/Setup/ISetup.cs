namespace Frogs.Instances.Setups
{
    public enum ViewMode { Single, SplitTop, SplitBottom }

    public struct Conditions
    {
        public ViewMode ViewMode { get; set; }
        public GM.Platform Platform { get; set; }
    }

    interface ISetup
    {
        void Setup(Conditions conditions);
    }
}