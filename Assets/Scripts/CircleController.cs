using System;
using System.Collections;
using System.Collections.Generic;
using Command;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleController : MonoBehaviour
{
    public float speed = 7.5f;

    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        InputHandler inputHandler = GetComponent<InputHandler>();
        if (inputHandler is null || !inputHandler.enabled)
        {
            Move();
        }
    }

    private void Move()
    {
        float random = Random.Range(-1, 1);

        _rigidbody2D.AddForce(
            random < 0
                ? Vector2.ClampMagnitude(new Vector2(-speed, random * speed), speed)
                : Vector2.ClampMagnitude(new Vector2(speed, random * speed), speed));
    }

    public void Move(float x, float y)
    {
        _rigidbody2D.velocity = new Vector2(x * speed, y * speed);
    }

    public void Move(Vector2 target)
    {
        _rigidbody2D.MovePosition(Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime)); 
    }
}
