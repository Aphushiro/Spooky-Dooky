using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public float moveSpeed = 5f;

    Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (canMove == false) { return; }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void ToggleCanMove()
    {
        canMove = !canMove;
    }
}