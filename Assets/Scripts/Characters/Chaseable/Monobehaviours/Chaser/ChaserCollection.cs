using UnityEngine;
using Util.Generic;
using System.Collections.Generic;

namespace Chaseables.MonoBehaviours
{
    public abstract class ChaserCollection : SerialisedInterfaceList<Chaser, IChaser>,
        IChaserCollection
    {
        protected List<Chaser> chaserMonoBehaviours => monoBehaviourList;
        protected List<IChaser> chasers => interfaceList;

        virtual public IChaseableCollection Chasing { get; set; }

        abstract public IChaser Chase(IChaseable chaseable);

        abstract public void ChaseableStartedLevel(IChaseable chaseable);

        abstract public IChaser GetFirstBehindOrNew(float pos);

        abstract public IChaser GetFirstBehindOrNull(float pos);
    }
}
