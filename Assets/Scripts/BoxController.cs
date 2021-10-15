using System;
using Manager;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(1);
            gameObject.SetActive(false);
        }
    }
}