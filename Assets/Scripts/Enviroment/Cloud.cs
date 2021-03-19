using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts {
    public class Cloud : MonoBehaviour
    {
        [SerializeField] GameObject spriteHolder;
        [SerializeField] CloudManager manager;

        float speed;
        float RandomSpeed => Random.Range(
            manager.averageSpeed - manager.speedVariance,
            manager.averageSpeed + manager.speedVariance
            );

        Animator animatior;

        static readonly Vector2 respawnWaitRange = new Vector2(1f, 3);
        float respawnWait = 2;
        float respawnTimer = 0;
        float RandomRespawnTime => Random.Range(respawnWaitRange.x, respawnWaitRange.y);
        bool respawning = false;

        const float maxSpawnPastLevel = 5;
        float spawnPos;

        const float minResetPos = -20;
        float resetPos;
        bool pastResetPos => transform.position.x < resetPos;

        const float minSpawnPositionDistance = 10;


        private void Start()
        {
            manager.Clouds.Add(this);

            speed = RandomSpeed;
            animatior = spriteHolder.GetComponent<Animator>();

            if (manager.randomPositions)
                PickNewPositions();
            else
            {
                spawnPos = manager.region.end;
                resetPos = manager.region.start;
            }

            RandomRespawnTimer();
        }

        private void Update()
        {
            if (respawning)
            {
                respawnTimer += Time.deltaTime;
                if (respawnTimer > respawnWait)
                {
                    PopUp();
                    respawning = false;
                    respawnTimer = 0;
                }
            }

            else if (pastResetPos)
            {
                Disappear();
            }
        }

        void FixedUpdate()
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }

        void RandomRespawnTimer() => respawnWait = Random.Range(manager.randomRespawnTime.x, manager.randomRespawnTime.y);

        void PickNewPositions()
        {
            float minSpawnPos = minResetPos + minSpawnPositionDistance;
            float maxSpawnPos = maxSpawnPastLevel + manager.region.end;
            spawnPos = Random.Range(minSpawnPos, maxSpawnPos);

            float maxResetPos = spawnPos - minSpawnPositionDistance;
            resetPos = Random.Range(minResetPos, maxResetPos);
        }

        public void PopUp()
        {
            transform.position = new Vector2(spawnPos, transform.position.y);
            animatior.SetTrigger("PopUp");

            if (manager.randomPositions)
                PickNewPositions();

            RandomRespawnTimer();

            speed = RandomSpeed;
        }

        void Disappear()
        {
            animatior.SetTrigger("Disappear");
            respawnWait = RandomRespawnTime;
            respawning = true;
        }
    }
}
