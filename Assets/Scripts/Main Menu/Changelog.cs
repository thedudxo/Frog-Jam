using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Changelog : MonoBehaviour
{

    [SerializeField] TextAsset[] seriousChangelogs;
    [SerializeField] TextAsset[] stupidChangelogs;

    [SerializeField] Text displayText;

    [SerializeField] Dropdown dropdown;

    bool stupidity = true;
    int version;         //starts at 0 = 0.2.2, increments by 1 each public release

    // Start is called before the first frame update
    void Start()
    {
        version = dropdown.options.Count - 1;
        ChangeText();
    }

    public void ToggleStupidity(Toggle toggle)
    {
        stupidity = toggle.isOn;
        ChangeText();
    }

    public void DropDownChanged()
    {
        version = (dropdown.options.Count - dropdown.value) - 1;
        ChangeText();
    }

    void ChangeText()
    {
        if (stupidity)
        {
            displayText.text = stupidChangelogs[version].text;
        }
        else
        {
            displayText.text = seriousChangelogs[version].text;
        }
    }
}
