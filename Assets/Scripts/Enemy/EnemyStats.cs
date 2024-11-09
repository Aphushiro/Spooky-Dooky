using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStats : MonoBehaviour
{
    public float health;
    public int hungerCount;
    public bool miniBoss = false;
    public int devourPower = 0;

    public void Takedamage (float damage, Vector3 sourcePos, float knockBackAmount)
    {
        health -= damage;
        if (gameObject.GetComponent<Rigidbody2D>() != null )
        {
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            Vector2 dir = (Vector2)(transform.position - sourcePos).normalized * knockBackAmount;
            rb.AddForce(dir, ForceMode2D.Impulse);
        }

        if (health <= 0)
        {
            Die();
        }

        if (gameObject.GetComponent<RedAnimation>() != null)
        {
            gameObject.GetComponent<RedAnimation>().ActivateRedTintFeedback();
        }
    }

    public int GetDevoured (float damage, Vector3 sourcePos, float knockBackAmount)
    {

        if (miniBoss)
        {
            Takedamage(0, sourcePos, knockBackAmount);
            return 0;
        } else
        {
            Takedamage(damage, sourcePos, knockBackAmount);
            return devourPower;
        }
    }

    void Die ()
    {
        PlayerStats.Instance.PlayerTakedown(hungerCount);
        GetComponent<SfxPlayer>().PlaySfx();
        Destroy(gameObject);
    }
}
