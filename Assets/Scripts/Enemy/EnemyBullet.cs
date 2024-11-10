using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;

    private Transform player;
    private Vector2 target;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        rb = gameObject.GetComponent<Rigidbody2D>();

        // Find direction of bullet
        Vector2 dir = new Vector2(target.x - rb.position.x, target.y - rb.position.y).normalized;
        rb.AddForce(dir * bulletSpeed, ForceMode2D.Impulse);
        //rb.AddTorque(0.5f, ForceMode2D.Impulse);

        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(1);
            Destroy(gameObject);
        }
    }

}
