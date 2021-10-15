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
        
        public Transform circle;
        public GameObject boxPrefab;
        public float minDistance = 2.0f;
        public Transform[] walls;

        private void Start()
        {
            _boxFactory = GetComponent<BoxFactory>();

            _maxBoxes = Mathf.FloorToInt(Random.Range(1, 20));

            for (int i = 0; i < _maxBoxes; i++)
            {
                SpawnBox();
            }
        }

        private void SpawnBox()
        {
            Vector2 randomPos;
            do
            {
                randomPos = RandomizePosition();
            } while (Vector2.Distance(randomPos, circle.position) < minDistance);

            _boxFactory.Produce(boxPrefab, randomPos, transform);
        }

        private Vector2 RandomizePosition()
        {
            float xPos = Random.Range(walls[0].position.x, walls[1].position.x);
            float yPos = Random.Range(walls[2].position.y, walls[3].position.y);

            return new Vector2(xPos, yPos);
        }
    }
}