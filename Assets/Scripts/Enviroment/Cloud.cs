using UnityEngine;
using Utils.Generic;

namespace Levels
{
    public class Cloud : MonoBehaviour
    {
        [SerializeField] GameObject spriteHolder;
        [SerializeField] CloudManager manager;

        Animator animatior;

        float speed;
        float RandomiseSpeed() => speed = RandomUtil.Variance(manager.averageSpeed, manager.speedVariance);


        float respawnTime = 2;
        float respawnTimer = 0;
        bool RespawnTimeElapsed => TimerUtil.UpdateTick(ref lifeTimer, lifetime);
        void RandomiseRespawnTime() => respawnTime = RandomUtil.Vector2(manager.randomRespawnTime);


        float lifetime = 10;
        float lifeTimer = 0;
        bool LifeTimeElapsed => TimerUtil.UpdateTick(ref respawnTimer, respawnTime);
        void RandomiseLifetime() => lifetime = RandomUtil.Vector2(manager.randomLifeTime);


        float spawnPos;
        void RandomiseSpawnPos() => spawnPos = Random.Range(manager.region.start, manager.region.end);


        public enum State { lifetime, respawning }
        public State state = State.lifetime;


        bool LeftRegion => transform.position.x < manager.region.start;

        void Start()
        {
            manager.Clouds.Add(this);

            animatior = spriteHolder.GetComponent<Animator>();

            RandomiseLifetime();
            RandomiseRespawnTime();
            RandomiseSpawnPos();
            RandomiseSpeed();
        }

        void FixedUpdate() => transform.position = new Vector3(transform.position.x + speed, transform.position.y,transform.position.z);

        private void Update()
        {
            switch (state)
            {
                case State.lifetime:

                    if (RespawnTimeElapsed)
                    {
                        RandomiseRespawnTime();
                        lifeTimer = 0;
                        EndLife();
                        state = State.respawning;
                    }

                    else if (LeftRegion)
                    {
                        transform.position = new Vector3(manager.region.end, transform.position.y, transform.position.z);
                    }

                    break;

                case State.respawning:

                    if (LifeTimeElapsed)
                    {
                        RandomiseLifetime();
                        respawnTimer = 0;
                        StartLife();
                        state = State.lifetime;
                    }

                    break;
            }
        }


        void StartLife()
        {
            RandomiseSpawnPos();
            RandomiseSpeed();

            transform.position = new Vector3(spawnPos, transform.position.y, transform.position.z);

            animatior.SetTrigger("PopUp");
        }

        void EndLife()
        {
            animatior.SetTrigger("Disappear");
        }
    }
}
