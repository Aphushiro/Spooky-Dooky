using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class PlayerBullet : MonoBehaviour
{
    private bool pierce = false;
    private float damage;
    private float knockback;
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    public void SetValues(bool p, float dmg, float knock)
    {
        pierce = p;
        damage = dmg;
        knockback = knock;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().Takedamage(damage, transform.position, knockback);

            // If piercing, the bolt should continue
            if (pierce) { return; }
            Destroy(gameObject);
        }
    }
}
