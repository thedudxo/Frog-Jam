using UnityEngine;

namespace Pursuits.MonoBehaviours
{
    public class PursuitMonoBehaviour : MonoBehaviour
    {
        [SerializeField] GameObject tempExaple;

        Pursuit pursuit;

        private void Awake()
        {
            pursuit = new Pursuit(new TempMovement());
        }
    }
}
