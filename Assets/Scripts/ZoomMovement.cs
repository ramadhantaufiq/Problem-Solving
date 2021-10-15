using System.Collections;
using Command;
using UnityEngine;
using UnityEngine.UI;

public class ZoomMovement : MonoBehaviour
{
    public CircleMovement circleMovement;
    public Slider zoomBar;

    private Animator _animator;
    
    public bool zoomActive;
    public float maxCharge = 100.0f;
    public float zoomMultiplier = 5.0f;
    public float zoomDuration = 0.2f;
    
    private float _zoomCharge;
    private float _normalSpeed;
    
    private static readonly int Zooming = Animator.StringToHash("Zooming");

    private void Start()
    {
        circleMovement = GetComponent<CircleMovement>();
        _animator = GetComponent<Animator>();
        
        _normalSpeed = circleMovement.speed;
        zoomActive = false;
        zoomBar.maxValue = maxCharge;
        UpdateCharge(0);
        
        InputHandler inputHandler = GetComponent<InputHandler>();
        if (inputHandler is null || !inputHandler.enabled)
        {
            circleMovement.Move();
        }
    }

    public void Zoom()
    {
        if (_zoomCharge < maxCharge || zoomActive)
        {
            return;
        }
        
        zoomActive = true;
        _animator.SetTrigger(Zooming);
        circleMovement.speed *= zoomMultiplier;
        UpdateCharge(-100);

        StartCoroutine(ResetSpeed());
    }

    public void UpdateCharge(int value)
    {
        float updatedCharge = _zoomCharge + value;
        _zoomCharge = Mathf.Clamp(updatedCharge, 0, maxCharge);
        zoomBar.value = _zoomCharge;
        
    }

    private IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(zoomDuration);

        circleMovement.speed = _normalSpeed;
        zoomActive = false;
    }
}
