using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExplosion : MonoBehaviour
{
    public string targetString;
    void Start()
    {
        if (targetString == "Player")
        {
            DamagePlayer();
        }

        if (targetString == "Enemy")
        {
            DamageEnemy();
        }
        // Play sound
        Destroy(gameObject, 0.25f);
    }

    private void DamageEnemy ()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag(targetString))
            {
                float[] fStats = PlayerStats.Instance.GetFireball();
                col.GetComponent<EnemyStats>().Takedamage(fStats[0], transform.position, 5f);
            }
        }
    }

    private void DamagePlayer()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag(targetString))
            {
                PlayerStats.Instance.TakeDamage(1);
            }
        }
    }
}
