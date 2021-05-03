using System.Collections;
using UnityEngine;
using Pursuits;

namespace Waves
{
    public class WavePursuer : MonoBehaviour, INotifyOnMemberRemoved
    {
        [SerializeField] Wave wave;
        public Pursuer pursuer;

        public void Setup(Pursuer pursuer)
        {
            this.pursuer = pursuer;
            this.pursuer.speed = wave.controllers.movement.speed;
            this.pursuer.ToNotifyOnMemberRemoved = this;
        }

        private void Update()
        {
            if (pursuer == null) return;
            pursuer.position = transform.position.x;
        }

        public void OnMemberRemoved()
        {
            wave.breakControlls.BreakWave();
            pursuer = null;
        }
    }
}