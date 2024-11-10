using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public float bulletSpeed;

    private Vector2 target;
    private Rigidbody2D rb;

    public GameObject EnemExplosion;

    // Start is called before the first frame update
    void Start()
    {
        float rn = Random.Range(0, Mathf.PI * 2);

        float x = Mathf.Sin(rn);
        float y = Mathf.Cos(rn);

        target = new Vector2 (x, y);

        rb = gameObject.GetComponent<Rigidbody2D>();

        // Find direction of bullet
        rb.AddForce(target * bulletSpeed, ForceMode2D.Impulse);
        rb.AddTorque(5);
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2f);
        GameObject fBall = Instantiate(EnemExplosion, transform.position, Quaternion.identity);
        fBall.GetComponent<FireExplosion>().targetString = "Player";
        Destroy(gameObject);
    }
}
