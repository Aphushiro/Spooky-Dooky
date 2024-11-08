using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public float damage;
    public float knockback = 5f;
    Rigidbody2D rb;

    float damagetimer = 1f;

    private void Start()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        if (damagetimer > 0f)
        {
            damagetimer -= Time.deltaTime;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && damagetimer <= 0f)
        {
            PlayerStats.Instance.TakeDamage(damage);
            damagetimer += 1f;

            if (rb == null)
                return;

            Vector2 dir = (Vector2)(transform.position - other.transform.position).normalized * knockback;
            rb.AddForce(dir, ForceMode2D.Impulse);
        }
    }
}
