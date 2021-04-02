using System.Collections.Generic;
using UnityEngine;
using Util.Generic;

namespace Chaseable.MonoBehaviours
{
    public class ChaseableCompositeCollection : 
        SerialisedInterfaceList<ChaseableCollection, IChaseableCollection>, 
        IChaseableCollection
    {
        List<ChaseableCollection> chaseableMonoBehaviours => monoBehaviourList;
        List<IChaseableCollection> chaseableCollections => interfaceList;


        List<IChaseable> IChaseableCollection.GetAll()
        {
            var chaseables = new List<IChaseable>();
            foreach (var collection in chaseableCollections)
            {
                //composite collections can't contain more composite collections, so this shouldn't be recursive
                foreach (var chaseable in collection.GetAll())
                { 
                    chaseables.Add(chaseable);
                }
            }

            return chaseables;
        }

        IChaseable IChaseableCollection.GetLastCanChase()
        {
            throw new System.NotImplementedException();
        }
    }
}