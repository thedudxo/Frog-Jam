using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelScripts {
    public class Cloud : MonoBehaviour
    {

        public float speed;
        const float averageSpeed = -0.02f;
        const float speedVariance = 0.005f;
        float RandomSpeed => Random.Range(averageSpeed - speedVariance, averageSpeed + speedVariance);

        Animator animatior;
        [SerializeField] GameObject spriteHolder;
        [SerializeField] Clouds cloudmanager;

        static readonly Vector2 respawnWaitRange = new Vector2(1f, 3);
        float respawnWait = 2;
        float respawnTimer = 0;
        float RandomRespawnTime => Random.Range(respawnWaitRange.x, respawnWaitRange.y);
        bool respawning = false;

        const float maxSpawnPastLevel = 5;
        float spawnPos;

        const float minResetPos = -20;
        float resetPos;

        const float minSpawnPositionDistance = 5;


        private void Start()
        {
            speed = RandomSpeed;
            animatior = spriteHolder.GetComponent<Animator>();
            PickNewPositions();
        }

        private void Update()
        {
            if (transform.position.x < resetPos)
            {
                Disappear();
            }

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
        }

        void FixedUpdate()
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }

        void PickNewPositions()
        {
            spawnPos = Random.Range(minResetPos + minSpawnPositionDistance, maxSpawnPastLevel + cloudmanager.level.end);
            resetPos = Random.Range(minResetPos, spawnPos - minSpawnPositionDistance);
        }

        public void PopUp()
        {
            transform.position = new Vector2(spawnPos, transform.position.y);
            animatior.SetTrigger("PopUp");

        }

        void Disappear()
        {
            animatior.SetTrigger("Disappear");
            PickNewPositions();
            respawnWait = RandomRespawnTime;
            respawning = true;
        }
    }
}
