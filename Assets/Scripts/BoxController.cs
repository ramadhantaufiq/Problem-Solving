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
            var playerController = other.GetComponent<PlayerCircleController>();
            if (playerController.zoomMovement != null)
            {
                playerController.zoomMovement.UpdateCharge(+10);
            }
            
            ScoreManager.Instance.AddScore(1);
            gameObject.SetActive(false);
            OnBoxDisabled();
            OnBoxDisabled = delegate {  };
        }
    }
}