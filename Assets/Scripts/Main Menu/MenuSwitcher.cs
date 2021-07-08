using UnityEngine;

namespace Menus
{
    public class MenuSwitcher : MonoBehaviour
    {
        GameObject currentActive;
        [SerializeField] GameObject StartMenu;

        void Start()
        {
            Cursor.visible = true;
            currentActive = StartMenu;
        }

        public void GoToMenu(GameObject menu)
        {
            currentActive.SetActive(false);
            menu.SetActive(true);
            currentActive = menu;
        }

        public void quitGame()
        {
            Application.Quit();
        }
    }
}
