using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> _pool = new List<GameObject>();

    public GameObject CheckPool()
    {
        return _pool.Find(obj => !obj.activeSelf);
    }

    public void AddToPool(GameObject obj)
    {
        _pool.Add(obj);
    }
}