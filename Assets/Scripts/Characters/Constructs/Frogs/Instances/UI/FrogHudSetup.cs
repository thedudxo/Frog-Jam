using UnityEngine;
using UnityEngine.UI;
using static GM.Platform;

namespace Frogs.Instances.UI
{
    class FrogHudSetup : MonoBehaviour
    {
        [SerializeField] RectTransform mainUIPanel;
        [SerializeField] CanvasScaler scaler;
        [SerializeField] new Camera camera;
        [SerializeField] Frog frog;

        //idk why this exact number, but it's what works.
        const float YOffsetRatio = 2.8f;

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
                    break;

                case ViewMode.SplitBottom:
                    SetOffset(-offset, -offset);
                    break;
            }
        }


        /// <summary> main UI panel is normaly half the screen size </summary>
        //void AdjustUISizeToFillScreen(float scale)
        //{
        //    float offset = (Screen.height / 2) / scale;
        //    SetOffset(offset, -offset);
        //}

        //public void MoveUiToTop()
        //{
        //    float offset = (Screen.height / YOffsetRatio);
        //    SetOffset(offset, offset);
        //}

        //public void MoveUiToBottom()
        //{
        //    float offset = (Screen.height / YOffsetRatio);
        //    SetOffset(-offset, -offset);
        //}

        void SetOffset(float topOffset, float bottomOffset)
        {
            mainUIPanel.offsetMax = new Vector2(mainUIPanel.offsetMax.x, topOffset);
            mainUIPanel.offsetMin = new Vector2(mainUIPanel.offsetMin.x, bottomOffset);
        }
    }
}
