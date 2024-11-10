using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    public float speed;
    private Transform target;

    [HideInInspector]
    public Vector2 force;
    private Vector2 backForce;

    public float backPedalDist = 4f;

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
        Vector2 diff = (Vector2)target.position - rb.position;
        Vector2 direction = diff.normalized;
        force = direction * speed * Time.deltaTime;
        backForce = -direction * speed * Time.deltaTime * 2.5f;

        if (diff.magnitude < backPedalDist)
        {
            force += backForce;
        }

        rb.AddForce(force, ForceMode2D.Force);

        // Flip enemyGFX depending on direction

        if (rb.velocity.x >= 0.01f)
        {
            spriteGfx.flipX = true;
        }
        else if (rb.velocity.x >= -0.01f)
        {
            spriteGfx.flipX = false;
        }
    }
}
