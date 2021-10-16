using System;
using Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Manager
{
    public class BoxManager : MonoBehaviour
    {
        private BoxFactory _boxFactory;
        private int _maxBoxes;
        private ObjectPool _boxPool = new ObjectPool();
        
        public Transform circle;
        public BoxController boxPrefab;
        public bool interactable = true;
        public float minDistance = 2.0f;
        public Transform[] walls;

        public bool respawnEnabled = true;
        public float respawnCooldown = 3.0f;
        private float _respawnTimer;
        private int _activeBoxes = 0;

        private void Start()
        {
            _boxFactory = GetComponent<BoxFactory>();

            _maxBoxes = Mathf.FloorToInt(Random.Range(1, 20));

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
            } while (Vector2.Distance(randomPos, circle.position) < minDistance);
            
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