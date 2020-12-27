using UnityEngine.UI;
using UnityEngine;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] Text timeTaken, pbTimeTaken, deathCount, pbDeathCount;
    [SerializeField] GameObject endScreen;

    const string timeFormat = "F3";

    private void Awake()
    {
        GM.levelEndScreen = this;
    }

    public void Enable(float time, float pbTime, int deaths, int pbDeaths)
    {
        endScreen.SetActive(true);
        timeTaken.text = time.ToString(timeFormat);
        pbTimeTaken.text = pbTime.ToString(timeFormat);
        deathCount.text = deaths.ToString();
        pbDeathCount.text = pbDeaths.ToString();
    }

    public void Disable()
    {
        endScreen.SetActive(false);
    }
}
