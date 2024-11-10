using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public UnityEvent OnDeath;
    public static PlayerStats Instance;

    public HealthBar healthbar;

    public int maxHealth = 3;
    public int currentHealth = 3;
    public bool invincible = false;

    public int hunger = 2;
    int hungerScale = 4;
    int totalDevour = 0;

    public AudioClip[] hurtSounds;
    public AudioClip[] dieSound;

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


    public float[] GetPunch()
    {
        int defMod = 1 + punch;

        float damage = defMod * 1f;
        float range = (punch * .035f) + 1f;
        float knockBack = (defMod * 0.3f) + 3;
        float coolDown = Mathf.Clamp(4.1f - (defMod * 0.1f), 0.5f, 4.1f); // Linearly from 5 to 0.5 seconds cooldown

        float[] pStat = new float[4] { damage, range, knockBack, coolDown};
        return pStat;
    }

    // Ability stats
    public float[] GetWhirlwind()
    {
        int defMod = 1 + punch;

        float damage = defMod * 2f;
        float range = (punch * .05f) + 2f;
        float knockBack = (defMod * 1f) + 3;

        float basecd = 8f;
        float downBy = defMod * 0.1f;
        float coolDown = Mathf.Clamp(basecd - downBy, 0.9f, basecd);

        float[] wStat = new float[4] { damage, range, knockBack, coolDown };
        return wStat;
    }

    public float[] GetBulletHell()
    {
        int defMod = 1 + punch;

        float damage = defMod * 1.5f;
        float count = defMod * 0.5f;
        float knockBack = (defMod * 1f) + 3;

        float basecd = 1f;
        float downBy = defMod * 0.08f;
        float coolDown = Mathf.Clamp(basecd - downBy, 0.25f, basecd);

        float[] bStat = new float[4] { damage, count, knockBack, coolDown };
        return bStat;
    }

    public float[] GetFireball()
    {
        int defMod = 1 + punch;

        float damage = defMod * 1.5f;
        float count = defMod * 0.3f;

        float basecd = 1f;
        float downBy = defMod * 0.08f;
        float coolDown = Mathf.Clamp(basecd - downBy, 0.25f, basecd);

        float[] fStat = new float[3] { damage, count, coolDown };
        return fStat;
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
        totalDevour++;
        hunger = hungerScale + (hungerScale * totalDevour);
        punch++;
        // Upscale stats & Abilities?
    }

    public void SetUiCd(int type, float time)
    {

    }

    public void SetInvincible(bool setTo)
    {
        invincible = setTo;
    }

    public void TakeDamage (int damage)
    {
        if (invincible) { return; }

        GameObject sfx = new GameObject("_sfx");
        SfxPlayer sound = sfx.AddComponent<SfxPlayer>();
        sound.clips = hurtSounds;
        Instantiate(sfx, Camera.main.transform.position, Quaternion.identity);

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
        GameObject sfx = new GameObject("_sfx");
        SfxPlayer sound = sfx.AddComponent<SfxPlayer>();
        sound.clips = dieSound;
        Instantiate(sfx, Camera.main.transform.position, Quaternion.identity);

        OnDeath.Invoke();
        UiExitGame();
    }

    public void UiExitGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}