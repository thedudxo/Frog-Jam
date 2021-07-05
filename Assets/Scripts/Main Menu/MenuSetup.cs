using UnityEngine;

namespace Menus
{
    public class MenuSetup : MonoBehaviour
    {
        [SerializeField] GameObject PCMainMenuButtons;
        [SerializeField] GameObject MobileMainMenuButtons;

        private void Awake()
        {
            switch (GM.platform)
            {
                case GM.Platform.PC:
                    PCMainMenuButtons.SetActive(true);
                    MobileMainMenuButtons.SetActive(false);
                    break;

                case GM.Platform.Android:
                    MobileMainMenuButtons.SetActive(true);
                    PCMainMenuButtons.SetActive(false);
                    break;
            }
        }
    }
}
