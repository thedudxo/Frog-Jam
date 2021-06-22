using UnityEngine;
using UnityEngine.UI;
using static GM.Platform;

namespace Frogs.Instances.Setups
{
    class FrogHudSetup : MonoBehaviour, ISetup
    {
        [SerializeField] RectTransform mainUIPanel;
        [SerializeField] CanvasScaler scaler;
        [SerializeField] new Camera camera;
        [SerializeField] Frog frog;

        public void Setup(Conditions c)
        {
            float scale = 1;

            if (c.Platform == Android)
            {
                scale = 2;
            }

            scaler.scaleFactor = scale;
            float offset = (Screen.height / 4 ) / scale;

            switch (c.ViewMode)
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
