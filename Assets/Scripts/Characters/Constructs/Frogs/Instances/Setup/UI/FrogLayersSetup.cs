using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances.Setups
{
    public class FrogLayersSetup : MonoBehaviour
    {
        [SerializeField] List<GameObject> SetUILayerList;

        string layerName = GM.player1UILayer;
        int layer;

        public void Setup(ViewMode viewMode)
        {
            if(viewMode == ViewMode.SplitBottom)
            {
                layerName = GM.player2UILayer;
            }

            layer = LayerMask.NameToLayer(layerName);

            foreach (GameObject obj in SetUILayerList)
            {
                SetObjectUILayer(obj);
            }
        }

        public void SetObjectUILayer(GameObject obj)
        { 
            obj.layer = layer;

            foreach (Transform childObj in obj.transform)
            {
                SetObjectUILayer(childObj.gameObject);
            }
        }
    }
}
