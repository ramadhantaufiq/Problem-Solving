using UnityEngine;

namespace Factory
{
    public interface IFactory
    {
        GameObject Produce(GameObject prefab, Vector2 spawn, Transform parent);
    }
}
