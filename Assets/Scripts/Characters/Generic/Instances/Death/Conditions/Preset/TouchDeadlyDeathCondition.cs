using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Characters.Instances.Deaths
{
    public class TouchDeadlyDeathCondition : IDeathCondition
    {
        List<GameObject> currentlyTouching;
        IRespawnMethod respawnMethod;
        public bool Enabled { get; set; } = true;
        public TouchDeadlyDeathCondition(List<GameObject> currentlyTouching, IRespawnMethod respawnMethod)
        {
            this.currentlyTouching = currentlyTouching;
            this.respawnMethod = respawnMethod;
        }

        public DeathInformation Check()
        {
            if (!Enabled) return null;

            var deadly = from obj in currentlyTouching
                         where obj.tag == Waves.Wave.Tag
                         select obj;

            if (deadly.Count() > 0)
            {
                return new DeathInformation(respawnMethod, "Touched a wave");
            }

            else return null;
        }
    }
}
