using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    public class BoxFactory : MonoBehaviour, IFactory
    {
        public GameObject Produce(GameObject prefab, Transform parent, bool interactable)
        {
            GameObject newBox = Instantiate(prefab, parent, false);

            if (!interactable)
            {
                newBox.GetComponent<BoxController>().enabled = false;
                newBox.GetComponent<BoxCollider2D>().enabled = false;
                newBox.GetComponent<SpriteRenderer>().color = Color.black;
            }
            
            newBox.SetActive(true);

            return newBox;
        }
    }
}