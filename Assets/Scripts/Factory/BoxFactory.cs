using UnityEngine;

namespace Factory
{
    public class BoxFactory : MonoBehaviour, IFactory
    {
        public GameObject Produce(GameObject prefab, Vector2 spawn, Transform parent, bool interactable)
        {
            GameObject newBox = Instantiate(prefab, spawn, Quaternion.identity, parent);

            if (!interactable)
            {
                newBox.GetComponent<BoxController>().enabled = false;
                newBox.GetComponent<BoxCollider2D>().enabled = false;
                newBox.GetComponent<SpriteRenderer>().color = Color.black;
            }
            return newBox;
        }
    }
}