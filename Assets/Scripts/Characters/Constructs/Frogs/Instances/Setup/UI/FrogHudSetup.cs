using UnityEngine;
using UnityEngine.UI;
using static GM.Platform;

namespace Frogs.Instances.Setups
{
    class FrogHudSetup : MonoBehaviour
    {
        [SerializeField] RectTransform mainUIPanel;
        [SerializeField] CanvasScaler scaler;
        [SerializeField] new Camera camera;
        [SerializeField] Frog frog;

        private void Start()
        {
            float scale = 1;

            if (GM.platform == Android)
            {
                scale = 2;
            }

            scaler.scaleFactor = scale;
            float offset = (Screen.height / 4 ) / scale;

            switch (frog.ViewMode)
            {   
                case ViewMode.Single:
                    if (GM.platform == Android)
                    {
                        offset = (Screen.height / 2) / scale;
                        SetOffset(offset, -offset);
                    }
                    break;

                case ViewMode.SplitTop:
                    SetOffset(offset, offset);
                    if (GM.platform == Android)
                    {
                        mainUIPanel.rotation = Quaternion.Euler(0, 0, 180);
                    }
                    break;

                case ViewMode.SplitBottom:
                    SetOffset(-offset, -offset);
                    break;
            }
        }

        void SetOffset(float topOffset, float bottomOffset)
        {
            mainUIPanel.offsetMax = new Vector2(mainUIPanel.offsetMax.x, topOffset);
            mainUIPanel.offsetMin = new Vector2(mainUIPanel.offsetMin.x, bottomOffset);
        }
    }
}
