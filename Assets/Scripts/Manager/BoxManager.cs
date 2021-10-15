using System;
using System.Collections.Generic;
using Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class BoxManager : MonoBehaviour
    {
        #region Singleton
        private static BoxManager _instance = null;
        public static BoxManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<BoxManager>();

                    if (_instance == null)
                    {
                        Debug.LogError("ScoreManager not found!");
                    }
                }

                return _instance;
            }
        }
        #endregion

        private BoxFactory _boxFactory;
        private int _maxBoxes = 2;
        private ObjectPool _boxPool = new ObjectPool();
        private Transform _circle;
        
        [Header("Spawn Settings")]
        public BoxController boxPrefab;
        public float minDistance = 2.0f;
        public bool interactable = true;
        public GameObject wallContainer;
        public List<Transform> walls = new List<Transform>();

        [Header("Respawn Settings")]
        public bool respawnEnabled = true;
        public bool progressEnabled = true;
        public float respawnCooldown = 3.0f;
        private float _respawnTimer;
        private int _activeBoxes = 0;

        private void Start()
        {
            _circle = GameObject.FindGameObjectWithTag("Player").transform;
            _boxFactory = GetComponent<BoxFactory>();
            walls.AddRange(wallContainer.GetComponentsInChildren<Transform>());
            walls.RemoveAt(0);

            if (!progressEnabled)
            {
                _maxBoxes = Mathf.FloorToInt(Random.Range(1, 50));
            }

            while (_activeBoxes < _maxBoxes)
            {
                SpawnBox();
            }
        }

        private void Update()
        {
            _respawnTimer += Time.deltaTime;

            if (respawnEnabled && _activeBoxes < _maxBoxes && _respawnTimer >= respawnCooldown)
            {
                SpawnBox();
                _respawnTimer = 0.0f;
            }
        }

        public void IncreaseMax(int value)
        {
            _maxBoxes += value;
        }

        private void SpawnBox()
        {

            var newBox = _boxPool.CheckPool();
            if (newBox == null)
            {
                newBox = _boxFactory.Produce(boxPrefab.gameObject, transform, interactable);
                _boxPool.AddToPool(newBox);
            }
            
            Vector2 randomPos;
            do
            {
                randomPos = RandomizePosition();
            } while (Vector2.Distance(randomPos, _circle.position) < minDistance);

            newBox.transform.position = randomPos;
            newBox.SetActive(true);
            
            newBox.GetComponent<BoxController>().OnBoxDisabled += PrepareRespawn;
            
            _activeBoxes += 1;
        }

        private Vector2 RandomizePosition()
        {
            float xPos = Random.Range(walls[0].position.x+1, walls[1].position.x-1);
            float yPos = Random.Range(walls[2].position.y+1, walls[3].position.y-1);

            return new Vector2(xPos, yPos);
        }

        private void PrepareRespawn()
        {
            _activeBoxes -= 1;
            if (_respawnTimer >= respawnCooldown)
            {
                _respawnTimer = 0.0f;
            }
        }
    }
}