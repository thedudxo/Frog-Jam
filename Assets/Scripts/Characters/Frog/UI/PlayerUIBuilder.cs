using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{

    public static class PlayerObjectInstanceBuilder
    {
        const string nullError = "No component of given type on template object";

        public static  void Build(List<GameObject> Templates)
        {
            foreach (GameObject template in Templates)
                InstantiateTemplate(template);
        }

        public static List<TComponent> Build<TComponent>(List<GameObject> Templates)
            where TComponent : MonoBehaviour
        {
            List < TComponent > components = new List<TComponent>();

            foreach (GameObject template in Templates)
            {
                GameObject obj = InstantiateTemplate(template);

                TComponent instance = obj.GetComponent<TComponent>();

                if (instance == null)
                    Debug.LogError(nullError);
                else
                    components.Add(instance);
            }
            return components;
        }

        static GameObject InstantiateTemplate(GameObject template)
        {
            GameObject obj = GameObject.Instantiate(template);

            obj.SetActive(true);
            obj.transform.position = template.transform.position;

            //obj.layer = LayerMask.NameToLayer(frog.UILayer);

            return obj;
        }
    }
}