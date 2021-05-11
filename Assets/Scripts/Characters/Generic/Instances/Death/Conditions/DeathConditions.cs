using Characters.Instances.Deaths;
using System.Collections.Generic;
using System.Linq;

namespace Characters.Instances.Deaths
{
    public class DeathConditions
    {
        List<IDeathCondition> conditions;

        public DeathConditions (List<IDeathCondition> conditions)
        {
            this.conditions = conditions;
        }

        public DeathInformation Check()
        {
            var priorityDeath = from DeathCondition in conditions
                                let deathInformation = DeathCondition.Check()
                                where deathInformation != null
                                orderby deathInformation.Priority descending
                                select deathInformation;

            return priorityDeath.FirstOrDefault();
        }
    }
}
