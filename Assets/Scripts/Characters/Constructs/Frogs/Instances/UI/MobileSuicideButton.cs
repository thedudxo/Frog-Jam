using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSuicideButton : MonoBehaviour
{
    void Start()
    {

#if UNITY_ANDROID == false
gameObject.SetActive(false);
#endif
    }
}
