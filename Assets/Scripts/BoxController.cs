using System;
using Manager;
using UnityEngine;
using UnityEngine.Events;

public class BoxController : MonoBehaviour
{
    public UnityAction OnBoxDisabled = delegate {  };
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(1);
            gameObject.SetActive(false);
            OnBoxDisabled();
            OnBoxDisabled = delegate {  };
        }
    }
}