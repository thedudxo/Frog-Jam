using System.Collections.Generic;
using UnityEngine;

namespace FrogScripts
{

    public static class ObjectInstanceBuilder
    {
        public delegate void ExtndGameObjSetup(GameObject obj);

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
                AddToComponentList(components, obj);
            }
            return components;
        }
        public static List<TComponent> Build<TComponent>(List<GameObject> Templates, ExtndGameObjSetup extendSetup)
            where TComponent : MonoBehaviour
        {
            List<TComponent> components = new List<TComponent>();

            foreach (GameObject template in Templates)
            {
                GameObject obj = InstantiateTemplate(template, extendSetup);
                AddToComponentList(components, obj);
            }
            return components;
        }

        private static void AddToComponentList<TComponent>(List<TComponent> components, GameObject obj) where TComponent : MonoBehaviour
        {
            TComponent instance = obj.GetComponent<TComponent>();

            if (instance == null)
                Debug.LogError("No component of given type on template object");
            else
                components.Add(instance);
        }

        static GameObject InstantiateTemplate(GameObject template)
        {
            GameObject obj;
            obj = GameObject.Instantiate(template);

            obj.SetActive(true);
            obj.transform.position = template.transform.position;

            return obj;
        }

        static GameObject InstantiateTemplate(GameObject template, ExtndGameObjSetup extendSetup)
        {
            GameObject obj;
            obj = InstantiateTemplate(template);
            extendSetup(obj);  //obj.layer = LayerMask.NameToLayer(frog.UILayer);
            return obj;
        }
    }
}