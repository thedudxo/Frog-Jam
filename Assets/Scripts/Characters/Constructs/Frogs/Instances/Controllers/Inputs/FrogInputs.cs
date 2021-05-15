using UnityEngine;

namespace Frogs.Instances.Inputs
{
    public class FrogInputs : MonoBehaviour
    {
        [SerializeField] public FrogSuicideInput suicide;
        [SerializeField] public Jump jump;
        [SerializeField] public QuitToMenu quitToMenu;
    }
}