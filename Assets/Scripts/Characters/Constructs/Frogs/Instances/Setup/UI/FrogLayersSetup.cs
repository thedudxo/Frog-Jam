using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frogs.Instances.Setups
{
    public class FrogLayersSetup : MonoBehaviour, ISetup
    {
        [SerializeField] List<GameObject> SetUILayerList;
        
        int layer;

        public void Setup(Conditions c)
        {
            layer = GetUILayer(c);

            SetObjectLayersInTaskList();
        }

        private static int GetUILayer(Conditions c)
        {
            string layerName = GM.player1UILayer;

            if (c.ViewMode == ViewMode.SplitBottom)
            {
                layerName = GM.player2UILayer;
            }

            return LayerMask.NameToLayer(layerName);
        }

        private void SetObjectLayersInTaskList()
        {
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
