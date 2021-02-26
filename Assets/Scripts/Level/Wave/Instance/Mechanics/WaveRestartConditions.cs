using System.Collections;
using UnityEngine;
using FrogScripts;

namespace waveScripts
{
    public class WaveRestartConditions : MonoBehaviour
    {
        [SerializeField] Wave wave;
        FrogManager frogManager;

        Transform waveTransform;
        bool ReachedEndOfLevel => waveTransform.position.x > wave.level.end;
        bool shouldRestart = false;


        private void Start()
        {
            frogManager = wave.frogManager;
            waveTransform = wave.transform;
            Debug.Log(waveTransform);
            Debug.Log(wave.level,wave.level);
        }

        private void Update()
        {
            if (ReachedEndOfLevel) shouldRestart = true;

            if (shouldRestart)
            {
                shouldRestart = false;
                wave.BreakWave();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(GM.playerTag))
            {
                CheckIfHitFrog(collision);
            }
        }

        private void CheckIfHitFrog(Collider2D collision)
        {
            Frog hitFrog = frogManager.GetFrogFromGameobject(collision.gameObject);

            if (hitFrog != null)
                if (frogManager.FrogIsFirst(hitFrog))
                    shouldRestart = true;
        }
    }
}