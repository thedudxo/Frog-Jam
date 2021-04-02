using System.Collections.Generic;
using System.Linq;
using Util.Generic;

namespace Chaseable.MonoBehaviours
{
    public class ChaseableCollection : 
        SerialisedInterfaceList<Chaseable, IChaseable>,
        IChaseableCollection
    {
        List<Chaseable> chaseableMonoBehaviours => monoBehaviourList;
        List<IChaseable> chaseables => interfaceList;


        public List<IChaseable> GetAll() => chaseables;

        public IChaseable GetLastCanChase()
        {
            var LastChaseableQuery =
                from chaseable in chaseableMonoBehaviours
                where chaseable.CanChase
                orderby chaseable.GetXPos() descending
                select chaseable;

            return LastChaseableQuery.Last();
        }
    }
}