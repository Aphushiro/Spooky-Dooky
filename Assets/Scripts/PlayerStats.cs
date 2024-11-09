using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public UnityEvent OnDeath;
    public static PlayerStats Instance;

    public HealthBar healthbar;

    public int maxHealth = 3;
    public int currentHealth = 3;

    // Stat upgrades
    public float currentMana = 0f;
    public float maxMana = 1f;

        // Stat upgrades count
    // Punch
    public int punch = 0;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public float[] GetPunch()
    {
        int defMod = 1 + punch;

        float damage = defMod * 1f;
        float range = defMod * 1f;
        float knockBack = defMod * 1f;

        float[] pStat = new float[3] { damage, range, knockBack };
        return pStat;
    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void HealDamage(float damage)
    {
        int intDmg = Mathf.CeilToInt(damage);

        currentHealth += intDmg;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthbar.SetHealth(currentHealth);
    }

    // Death or other meta stuff ------
    public void Die()
    {
        OnDeath.Invoke();
        StartCoroutine(ResetWorld());
    }

    IEnumerator ResetWorld()
    {
        // Hard coded to match load screen time
        yield return new WaitForSeconds(2f);
        RespawnPlayer();
    }

    public void RespawnPlayer ()
    {

    }
}
