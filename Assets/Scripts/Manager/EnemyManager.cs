using System.Collections.Generic;
using Factory;
using UnityEngine;

namespace Manager
{
    public class EnemyManager : MonoBehaviour
    {

        #region Singleton

        private static EnemyManager _instance = null;

        public static EnemyManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EnemyManager>();

                    if (_instance == null)
                    {
                        Debug.LogError("EnemyManager not found!");
                    }
                }

                return _instance;
            }
        }

        #endregion

        private EnemyFactory _enemyFactory;
        private ObjectPool _enemyPool = new ObjectPool();
        private Transform _circle;

        [Header("Spawn Settings")] 
        public EnemyCircleController enemyPrefab;
        public float minDistance = 5.0f;
        public GameObject wallContainer;
        public List<Transform> walls = new List<Transform>();

        [Header("Respawn Settings")] 
        public int maxEnemies = 0;
        public float respawnCooldown = 5.0f;
        private float _respawnTimer;
        private int _activeEnemies = 0;

        private void Start()
        {
            walls.AddRange(wallContainer.GetComponentsInChildren<Transform>());
            walls.RemoveAt(0);
            
            _circle = GameObject.FindGameObjectWithTag("Player").transform;
            _enemyFactory = GetComponent<EnemyFactory>();
        }

        private void Update()
        {
            _respawnTimer += Time.deltaTime;

            if (_activeEnemies < maxEnemies && _respawnTimer >= respawnCooldown)
            {
                Spawn();
                _respawnTimer = 0.0f;
            }
        }

        public void IncreaseMax(int value)
        {
            maxEnemies += value;
        }

        private void Spawn()
        {

            var newEnemy = _enemyPool.CheckPool();
            if (newEnemy == null)
            {
                newEnemy = _enemyFactory.Produce(enemyPrefab.gameObject, transform, true);
                _enemyPool.AddToPool(newEnemy);
            }

            Vector2 randomPos;
            do
            {
                randomPos = RandomizePosition();
            } while (Vector2.Distance(randomPos, _circle.position) < minDistance);

            newEnemy.transform.position = randomPos;
            newEnemy.SetActive(true);
            
            newEnemy.GetComponent<EnemyCircleController>().OnDisabled += PrepareRespawn;

            _activeEnemies += 1;
        }

        private Vector2 RandomizePosition()
        {
            float xPos = Random.Range(walls[0].position.x + 1, walls[1].position.x - 1);
            float yPos = Random.Range(walls[2].position.y + 1, walls[3].position.y - 1);

            return new Vector2(xPos, yPos);
        }

        private void PrepareRespawn()
        {
            _activeEnemies -= 1;
            if (_respawnTimer >= respawnCooldown)
            {
                _respawnTimer = 0.0f;
            }
        }
    }
}