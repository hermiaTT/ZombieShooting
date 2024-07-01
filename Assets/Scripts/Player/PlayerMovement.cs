using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;   

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private InputReader input;

    private Vector2 moveDirection;

    private Rigidbody2D rb_2d;

    private void Start()
    {
        input.MoveEvent += HandleMove;

        rb_2d = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void HandleMove(Vector2 direction)
    {
        moveDirection = direction;
    }


    private void Move()
    {
        rb_2d.velocity = moveDirection * speed * Time.deltaTime;
    }
}
