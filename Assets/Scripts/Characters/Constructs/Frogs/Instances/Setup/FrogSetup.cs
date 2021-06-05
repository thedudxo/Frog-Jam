using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frogs.Instances.UI;
using Frogs.Collections;

namespace Frogs.Instances
{
    public class FrogSetup : MonoBehaviour
    {

        [SerializeField] FrogHudSetup Hud;
        [SerializeField] ControllsTextSetup ControllsText;
        // camera
        // controll method based on splitscreen


        public void Setup()
        {
            Debug.Log(FrogStartSettings.veiwMode);
            Debug.Log(GM.platform);
        }
    }
}
