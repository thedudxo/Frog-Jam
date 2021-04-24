using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    GameObject currentActive;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        foreach(Transform t in transform)
        {
            if (t.gameObject.activeInHierarchy)
            {
                currentActive = t.gameObject;
                break;
            }
        }
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
