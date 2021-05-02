using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pursuits
{
    public class Runner : PursuitMember
    {
        public Pursuer pursuerBehind = null;

        public override string ToString()
        {
            return $"<color=green>Runner</color> at {position}";
        }
    }
}
