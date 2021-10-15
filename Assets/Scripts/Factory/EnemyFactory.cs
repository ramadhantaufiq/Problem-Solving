using UnityEngine;

namespace Factory
{
    public class EnemyFactory : MonoBehaviour, IFactory
    {
        public GameObject Produce(GameObject prefab, Transform parent, bool interactable)
        {
            GameObject newEnemy = Instantiate(prefab, parent);

            return newEnemy;
        }
    }
}