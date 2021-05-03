using System.Collections;
using UnityEngine;
using Pursuits;

namespace Waves
{
    public class WavePursuer : MonoBehaviour, INotifyOnMemberRemoved
    {
        [SerializeField] Wave wave;
        public Pursuer pursuer;
        bool setup = false;

        public void Setup(Pursuer pursuer)
        {
            this.pursuer = pursuer;
            this.pursuer.speed = wave.controllers.movement.speed;
            this.pursuer.ToNotifyOnMemberRemoved = this;
            setup = true;
        }

        private void Update()
        {
            //if (setup == false) return;
            pursuer.position = transform.position.x;
        }

        public void OnMemberRemoved()
        {
            wave.breakControlls.BreakWave();
            setup = false;
        }
    }
}