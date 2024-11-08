using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    private Transform target;

    [HideInInspector]
    public Vector2 force;

    Rigidbody2D rb;
    SpriteRenderer spriteGfx;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
    }



    private void FixedUpdate()
    {
        
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
