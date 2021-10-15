using UnityEngine;

namespace Factory
{
    public interface IFactory
    {
        GameObject Produce(GameObject prefab, Transform parent, bool interactable);
    }
}
