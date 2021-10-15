using UnityEngine;

namespace Factory
{
    public class BoxFactory : MonoBehaviour, IFactory
    {
        public GameObject Produce(GameObject prefab, Vector2 spawn, Transform parent)
        {
            return Instantiate(prefab, spawn, Quaternion.identity, parent);
        }
    }
}