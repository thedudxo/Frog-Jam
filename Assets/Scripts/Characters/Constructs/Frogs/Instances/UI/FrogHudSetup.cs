using UnityEngine;
using UnityEngine.UI;

namespace Frogs.Instances.UI
{
    class FrogHudSetup : MonoBehaviour
    {
        [SerializeField] RectTransform mainUIPanel;
        [SerializeField] CanvasScaler scaler;

#if UNITY_ANDROID
        private void Awake()
        {
            float scale = 2;
            scaler.scaleFactor = scale;

            //if singleplayer
            AdjustUISizeToFillScreen(scale);
        }
#endif

        /// <summary> main UI panel is normaly half the screen size </summary>
        void AdjustUISizeToFillScreen(float scale)
        {
            float offset = (Screen.height / 2) / scale;
            mainUIPanel.offsetMax = new Vector2(mainUIPanel.offsetMax.x, offset);
            mainUIPanel.offsetMin = new Vector2(mainUIPanel.offsetMin.x, -offset);
        }
    }
}
