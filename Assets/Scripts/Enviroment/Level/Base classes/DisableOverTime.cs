using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisableOverTime : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] float displayTimeSeconds = 3;
    float timer;
    bool notTiming => !obj.activeInHierarchy;
    bool timeUp => timer >= displayTimeSeconds;

    private void Start()
    {
        obj.SetActive(false);
    }

    public void EnableObject()
    {
        timer = 0;
        obj.SetActive(true);
    }

    private void Update()
    {
        if (notTiming) return;

        timer += Time.deltaTime;

        if (timeUp)
        {
            obj.SetActive(false);
            timer = 0;
        }
    }
}