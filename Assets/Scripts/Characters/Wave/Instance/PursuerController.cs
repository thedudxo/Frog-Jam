using System.Collections;
using UnityEngine;
using Pursuits;

namespace WaveScripts
{
    public class PursuerController : MonoBehaviour, INotifyOnMemberRemoved
    {
        [SerializeField] Wave wave;
        public Pursuer pursuer;

        private void Start()
        {
            pursuer.speed = wave.controllers.movement.speed;
            pursuer.ToNotifyOnMemberRemoved = this;
        }

        private void Update()
        {
            pursuer.position = transform.position.x;
        }

        public void OnMemberRemoved()
        {
            wave.breakControlls.BreakWave();
        }
    }
}