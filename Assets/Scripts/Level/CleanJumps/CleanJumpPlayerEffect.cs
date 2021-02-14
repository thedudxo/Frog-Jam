using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FrogScripts
{
    public class CleanJumpPlayerEffect : MonoBehaviour
    {

        [SerializeField] Text congratulationsText;
        [SerializeField] float displayTimeSeconds;
        float timer;
        bool timing;

        private void Update()
        {
            if (!timing) return;

            timer += Time.deltaTime;
            if (timer >= displayTimeSeconds)
            {
                congratulationsText.enabled = false;
                timing = false;
            }
        }
    }
}