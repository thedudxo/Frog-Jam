using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.Generic;

namespace LevelScripts {
    public class Cloud : MonoBehaviour
    {
        [SerializeField] GameObject spriteHolder;
        [SerializeField] CloudManager manager;

        Animator animatior;

        float speed;
        float RandomiseSpeed() => speed = RandomUtil.Variance(manager.averageSpeed, manager.speedVariance);

        float respawnTime = 2;
        void RandomiseRespawnTime() => respawnTime = RandomUtil.Vector2(manager.randomRespawnTime);

        float lifetime = 10;
        void RandomiseLifetime() => lifetime = RandomUtil.Vector2(manager.randomLifeTime);

        float spawnPos;
        void RandomiseSpawnPos() => spawnPos = Random.Range(manager.region.start, manager.region.end);

        public enum State {lifespan, Hidden }
        public State state = State.lifespan;

        bool lifespanRunning = false;

        private void Start()
        {
            manager.Clouds.Add(this);

            animatior = spriteHolder.GetComponent<Animator>();

            RandomiseVariables();

            //was experimenting with coroutines. it was probably cleaner to just do it in update with a timer like usual.
            StartCoroutine(CheckCloudWithinRegion());
            StartCoroutine(Lifespan());
        }

        void FixedUpdate()
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }

        void Disappear(bool randomiseSpawnPos)
        {
            animatior.SetTrigger("Disappear");
            state = State.Hidden;
            StartCoroutine(Respawn(randomiseSpawnPos));
        }

        IEnumerator Respawn(bool randomiseSpawnPos)
        {
            yield return new WaitForSeconds(respawnTime);
            PopUp(randomiseSpawnPos);
        }
        public void PopUp(bool randomiseSpawnPos)
        {
            if (randomiseSpawnPos)
                RandomiseSpawnPos();
            else spawnPos = manager.region.end;

            transform.position = new Vector2(spawnPos, transform.position.y);

            animatior.SetTrigger("PopUp");

            if (lifespanRunning == false)
            {
                lifespanRunning = true;
                StartCoroutine(Lifespan());
            }

            state = State.lifespan;
        }

        IEnumerator Lifespan()
        {
            yield return new WaitForSeconds(lifetime);

            Debug.Log("Lifetime:" + lifetime);
            lifespanRunning = false;
            RandomiseVariables();
            Disappear(true);
        }

        void RandomiseVariables()
        {
            RandomiseRespawnTime();
            RandomiseSpeed();
            RandomiseLifetime();
        }

        IEnumerator CheckCloudWithinRegion()
        {
            for(;;) //unity why are you like this
            {
                if (state == State.lifespan)
                {
                    bool leftRegion = transform.position.x < manager.region.start;

                    if (leftRegion)
                    {
                        Disappear(false);
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
