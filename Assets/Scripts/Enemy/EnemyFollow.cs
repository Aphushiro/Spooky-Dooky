using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform target;

    [HideInInspector]
    public Vector2 force;

    Rigidbody2D rb;
    SpriteRenderer spriteGfx;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        spriteGfx = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        force = direction * speed * Time.deltaTime;

        rb.AddForce(force, ForceMode2D.Force);

        // Flip enemyGFX depending on direction

        if (rb.velocity.x >= 0.01f)
        {
            spriteGfx.flipX = true;
        } else if (rb.velocity.x >= -0.01f)
        {
            spriteGfx.flipX = false;
        }
    }
}
