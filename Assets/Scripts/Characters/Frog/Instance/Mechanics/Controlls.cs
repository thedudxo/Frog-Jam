using System.Collections;
using UnityEngine;

namespace FrogScripts
{
    public class Controlls : MonoBehaviour
    {
        [SerializeField] public KeyCode jumpKey = KeyCode.Space;
        [SerializeField] public KeyCode suicideKey = KeyCode.Q;

        [HideInInspector] public KeyCode QuitToMenuKey = KeyCode.Escape;

        void Update()
        {
            //quit to menu
            if (Input.GetKeyDown(QuitToMenuKey))
            {
                GM.QuitToMenu();
            }
        }
    }
}