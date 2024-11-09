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

    public int hunger = 10;
    int hungerScale = 2;
    int totalDevour = 0;

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
        float range = (punch * .05f) + 2.3f;
        float knockBack = (defMod * 0.3f) + 3;
        float coolDown = Mathf.Clamp(4.1f - (defMod * 0.1f), 0.5f, 4.1f); // Linearly from 5 to 0.5 seconds cooldown

        float[] pStat = new float[4] { damage, range, knockBack, coolDown};
        return pStat;
    }

    // Ability stats
    public float[] GetWhirlwind()
    {
        int defMod = 1 + punch;

        float damage = defMod * 1.7f;
        float range = (punch * .05f) + 3f;
        float knockBack = (defMod * 1f) + 3;
        float coolDown = Mathf.Clamp(4.1f - (defMod * 0.1f), 0.5f, 4.1f); // Linearly from 5 to 0.5 seconds cooldown

        float[] wStat = new float[4] { damage, range, knockBack, coolDown };
        return wStat;
    }

    public void PlayerTakedown(int hungerAdd)
    {
        if (hunger > 0)
        {
            hunger -= hungerAdd;
        }
    }

    public void DevourSuccess()
    {
        hunger = 10 + (hungerScale * totalDevour);
        // Upscale stats & Abilities?
    }

    public void SetUiCd(int type, float time)
    {

    }

    public void TakeDamage (int damage)
    {
        currentHealth -= damage;
        //healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
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
    }
}
