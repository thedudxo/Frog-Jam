using UnityEngine;
using UnityEngine.UI;

namespace Levels.UI
{

    public class ControllsTextTip : MonoBehaviour
    {
        [SerializeField] Text jumpKeyTipText;
        [SerializeField] Text suicideKeyTipText;

        public void SetControlls(KeyCode jumpKey, KeyCode suicideKey)
        {
            string jumpText = jumpKey.ToString();
            string suicideText = suicideKey.ToString();

            //this is a really quick fix to make the keys display as a user freindly name
            if (jumpKey == KeyCode.UpArrow) jumpText = "up arrow";
            if (suicideKey == KeyCode.RightShift) suicideText = "shift";

            jumpKeyTipText.text = $"Press {jumpText} to Jump";
            suicideKeyTipText.text = $"Press {suicideText} to die";
        }
    }
}