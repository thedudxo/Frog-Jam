using UnityEngine;
using UnityEngine.UI;

namespace LevelScripts.UI
{

    public class ControllsTextTip : MonoBehaviour
    {
        [SerializeField] Text jumpKeyTipText;
        [SerializeField] Text suicideKeyTipText;

        public void SetControlls(KeyCode jumpKey, KeyCode suicideKey)
        {
            jumpKeyTipText.text = $"Jump = {jumpKey}";
            suicideKeyTipText.text = $"Suicide = {suicideKey}";
        }
    }
}