using UnityEngine;

namespace Frogs.Instances.Inputs
{

    public class FrogInputs : MonoBehaviour
    {
        [SerializeField] public FrogSuicideInput suicide;
        [SerializeField] public JumpInput jump;
        [SerializeField] public QuitToMenuInput quitToMenu;
        [SerializeField] public FrogLevelRestartInput levelRestart;
    }
}