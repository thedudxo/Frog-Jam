using UnityEngine;
using Frogs.Instances.State;

namespace Frogs.Instances.Inputs
{
    public class FrogLevelRestartInput : MonoBehaviour
    {
        [SerializeField] FrogStateContext context;
        public void RestartLevel()
        {
            context.endLevel.ExitState();
        }
    }
}