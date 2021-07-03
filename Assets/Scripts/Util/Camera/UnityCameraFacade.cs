using UnityEngine;

namespace Utils.Cameras
{
    /// <summary>
    /// allows testability of the extension method WorldOffsetFromScreenEdge() by decoupling unity
    /// </summary>
    public class UnityCameraFacade : IScreen
    {
        readonly Camera camera;

        public UnityCameraFacade(Camera camera)
        {
            this.camera = camera;
        }

        public float ScreenWidth => Screen.width;
        public Vector3 ScreenToWorld(Vector3 screenPos) 
        { 
            return camera.ScreenToWorldPoint(screenPos); 
        }
    }
}
