﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pursuits
{
    public class Pursuer : PursuitMember
    {
        public override string ToString()
        {
            return $"<color=blue>Pursuer</color> at {position}";
        }
    }
}
