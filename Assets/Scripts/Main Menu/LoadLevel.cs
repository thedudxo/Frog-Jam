using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class LoadLevel : MonoBehaviour
    {
        void LoadFirstLevel(GM.PlayerMode mode)
        {
            GM.playerMode = mode;
            SceneManager.LoadScene(1);
        }

        public void StartSingleplayer() => LoadFirstLevel(GM.PlayerMode.single);
        public void StartSplitscreen() => LoadFirstLevel(GM.PlayerMode.SplitScreen2);
    }
}
