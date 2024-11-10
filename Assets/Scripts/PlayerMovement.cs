using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public float moveSpeed = 5f;
    public Animator playerAnim;

    Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        playerAnim.SetFloat("speed", Mathf.Abs(movement.magnitude));


        if (movement.x > 0.1f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        
        if (movement.x < -0.1f)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
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