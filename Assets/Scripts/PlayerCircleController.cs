using Command;
using Manager;
using UnityEngine;

public class PlayerCircleController : MonoBehaviour
{
    public CircleMovement circleMovement;
    public ZoomMovement zoomMovement;

    private void Start()
    {
        circleMovement = GetComponent<CircleMovement>();
        zoomMovement = GetComponent<ZoomMovement>();
        
        InputHandler inputHandler = GetComponent<InputHandler>();
        if (inputHandler is null || !inputHandler.enabled)
        {
            circleMovement.Move();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameFlowManager.Instance.GameOver();
    }
}
