using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollNoStretch : MonoBehaviour
{

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Scrollbar scrollbar;

    public void OnScroll()
    {
        scrollRect.verticalNormalizedPosition = scrollbar.value;
    }

    public void OnDrag()
    {
        scrollbar.value = scrollRect.verticalNormalizedPosition;
    }
}
