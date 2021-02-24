using System.Collections;
using UnityEngine;

namespace waveScripts
{
    public class WaveMovement : MonoBehaviour
    {
        const float speed = 8f;
        Transform waveTransform;
        [SerializeField] Wave wave;

        private void Start()
        {
            waveTransform = wave.transform;
        }

        private void FixedUpdate()
        {
            waveTransform.position = new Vector2(
                transform.position.x + (speed * Time.deltaTime),
                transform.position.y);
        }
    }
}