using System.Collections.Generic;
using System.Linq;
using Util.Generic;

namespace Chaseables.MonoBehaviours
{
    public class ChaseableCollection : SerialisedInterfaceList<Chaseable, IChaseable>,
        IChaseableCollection
    {
        protected List<Chaseable> chaseableMonoBehaviours => monoBehaviourList;
        protected List<IChaseable> chaseables => interfaceList;


        public List<IChaseable> GetAll() => chaseables;

        public IChaseable GetLastCanChase()
        {
            var LastChaseableQuery =
                from chaseable in chaseableMonoBehaviours
                where chaseable.IsCurrentlyChaseable
                orderby chaseable.GetXPos() descending
                select chaseable;

            return LastChaseableQuery.Last();
        }
    }
}