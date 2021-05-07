using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public class FrogInputs : MonoBehaviour
    {
        [SerializeField] public Suicide suicide;
        [SerializeField] public Jump jump;
        [SerializeField] public QuitToMenu quitToMenu;
    }
}