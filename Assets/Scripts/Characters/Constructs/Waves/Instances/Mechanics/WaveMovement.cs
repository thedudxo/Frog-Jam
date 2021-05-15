using System.Collections;
using UnityEngine;

namespace Waves
{
    public class WaveMovement : MonoBehaviour
    {
        public float speed = 8f;
        Transform waveTransform;
        [SerializeField] Wave wave;

        private void Start()
        {
            waveTransform = wave.transform;
        }

        private void FixedUpdate()
        {
            if (wave.state == Wave.State.inactive) return;

            waveTransform.position = new Vector2(
                transform.position.x + (speed * Time.deltaTime),
                transform.position.y);
        }
    }
}