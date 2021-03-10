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
        public float respawnTimer = 0;
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
            speed = RandomSpeed;
            animatior = spriteHolder.GetComponent<Animator>();
            PickNewPositions();
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
                //Debug.Log($"POSITION: {transform.position.x} < {resetPos}");
                Disappear();
            }
        }

        void FixedUpdate()
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }

        void PickNewPositions()
        {
            float minSpawnPos = minResetPos + minSpawnPositionDistance;
            float maxSpawnPos = maxSpawnPastLevel + cloudmanager.level.end;
            spawnPos = Random.Range(minSpawnPos, maxSpawnPos);

            float maxResetPos = spawnPos - minSpawnPositionDistance;
            resetPos = Random.Range(minResetPos, maxResetPos);

            //Debug.Log($"SPAWN POS: {spawnPos}   RESET POS: {resetPos}");
        }

        public void PopUp()
        {
            transform.position = new Vector2(spawnPos, transform.position.y);
            animatior.SetTrigger("PopUp");
            PickNewPositions();
            //Debug.Log($"POPUP at {spawnPos}");

        }

        void Disappear()
        {
            animatior.SetTrigger("Disappear");
            respawnWait = RandomRespawnTime;
            respawning = true;
            //Debug.Log("DISAPPEAR");
        }
    }
}
