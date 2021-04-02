using System.Collections.Generic;
using UnityEngine;

namespace Util.Generic
{
    public class SerialisedInterfaceList<TMonoBehaviour, TInterface> : MonoBehaviour
        where TMonoBehaviour : TInterface
    {
        [SerializeField] protected List<TMonoBehaviour> monoBehaviourList;
        protected List<TInterface> interfaceList;

        virtual protected void OnValidate()
        {
            interfaceList.Clear();
            foreach (TInterface item in monoBehaviourList)
                interfaceList.Add(item);
        }

        virtual protected void Add(TMonoBehaviour item)
        {
            monoBehaviourList.Add(item);
            interfaceList.Add(item);
        }

        virtual protected void Remove(TMonoBehaviour item)
        {
            monoBehaviourList.Remove(item);
            interfaceList.Remove(item);
        }
    }
}