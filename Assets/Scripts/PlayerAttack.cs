using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    Vector3 cursorDiff;
    Vector3 cursorRotation;
    Animator playerAnim;

    bool canAttack = true;
    bool isFrenzy = false;

    bool autoAttackBlock = false;
    public GameObject attackAnimPrefab;
    public GameObject whirlwindAnimPrefab;
    public GameObject bulletPrefab;
    public GameObject friendlyFireball;

    [SerializeField]
    int attackType = 1; // None, Pitch, Musketeer, Torcher
    [SerializeField]
    bool blockAbility = false;

    bool devourReady = true; // Independant of whether or not hunger is ready
    bool isDevouring = false;

    public SfxPlayer[] sfxPlayers;

    private void Start()
    {
        playerAnim = GetComponentInChildren<Animator>();

        StartCoroutine(StopThatBug());
    }

    IEnumerator StopThatBug()
    {
        PlayerStats.Instance.SetInvincible(false);
        yield return new WaitForSeconds(10);
        StartCoroutine(StopThatBug());
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        cursorDiff = mousePos - transform.position;
        cursorDiff.Normalize();

        float rot_z = Mathf.Atan2(cursorDiff.y, cursorDiff.x) * Mathf.Rad2Deg;
        rot_z -= 90f;

        cursorRotation = new Vector3(0, 0, rot_z);

        // Perform Ability
        if (Input.GetMouseButtonDown(0))
        {
            AbilityMove();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Devour();
        }
    }

    private void FixedUpdate()
    {
        AutoAttack();
    }

    private void AbilityMove()
    {
        if (canAttack == false) { return; }
        if (blockAbility) { return; }

        blockAbility = true;
        switch (attackType) // None, Pitch, Musketeer, Torcher
        {
            case 1 :
                Whirlwind();
                break;

            case 2 :
                BulletHell();
                break;
            case 3 :
                FireballRain();
                break;
            default:
                break;
        }
    }

    private void AutoAttack()
    {
        if (canAttack == false) { return; }
        if (autoAttackBlock) { return; }

        float autoAttackCd = PlayerStats.Instance.GetPunch()[3];
        autoAttackBlock = true;
        StartCoroutine(DelayAttack(autoAttackCd));
    }
    private IEnumerator DelayAttack(float seconds)
    {
        Punch();
        sfxPlayers[0].PlaySfx();
        playerAnim.SetTrigger("Swipe");
        yield return new WaitForSeconds(seconds);
        autoAttackBlock = false;
    }

    private void Punch()
    {
        float[] pStats = PlayerStats.Instance.GetPunch();               // { damage, range, knockback, cooldown }
        float dist = pStats[1] + (transform.localScale.x / 2);

        Vector2 attackPos = transform.position + (cursorDiff * dist);
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPos, pStats[1]);

        // Punch GFX
        Vector3 attackSize = new Vector3(pStats[1], pStats[1], 1f) * 2;
        GameObject attackAnim = Instantiate(attackAnimPrefab, attackPos, Quaternion.Euler(cursorRotation));
        attackAnim.transform.localScale = attackSize;
        Destroy(attackAnim, 0.25f);


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Enemy"))
            {
                cols[i].GetComponent<EnemyStats>().Takedamage(pStats[0], transform.position, pStats[2]);
            }
        }

    }

    // Abilities
    IEnumerator AbilityCd(float time)
    {
        Debug.Log("Ability CD: " + time);
        PlayerStats.Instance.SetUiCd(attackType, time);
        yield return new WaitForSeconds(time);
        blockAbility = false;
    }

    private void Whirlwind()
    {
        float[] wStats = PlayerStats.Instance.GetWhirlwind();

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, wStats[1]);

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Enemy"))
            {
                cols[i].GetComponent<EnemyStats>().Takedamage(wStats[0], transform.position, wStats[2]);
            }
        }

        sfxPlayers[0].PlaySfx();

        // Punch GFX
        Vector3 attackSize = new Vector3(wStats[1], wStats[1], 1f) * 2;
        GameObject attackAnim = Instantiate(whirlwindAnimPrefab, transform.position, Quaternion.identity);
        attackAnim.transform.localScale = attackSize;
        Destroy(attackAnim, 0.25f);
        StartCoroutine(AbilityCd(wStats[3]));
    }

    private void BulletHell()
    {
        float speed = 9f;
        float[] bStats = PlayerStats.Instance.GetBulletHell();
        bool pierce = true;
        float rad = Mathf.Deg2Rad * 9f;

        for (int i = 0;i < Mathf.CeilToInt(bStats[1]) ;i++)
        {
            rad *= (i + 1);
            GameObject negBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D nRb = negBullet.GetComponent<Rigidbody2D>();

            GameObject posBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D pRb = posBullet.GetComponent<Rigidbody2D>();

            float cursorAng = Mathf.Atan2(cursorDiff.y, cursorDiff.x);

            Vector2 neg = new Vector2(Mathf.Cos(cursorAng + rad), Mathf.Sin(cursorAng + rad));
            Vector2 pos = new Vector2(Mathf.Cos(cursorAng - rad), Mathf.Sin(cursorAng - rad));

            //Debug.Log("Neg: " + neg + "\nPos: " + pos);

            nRb.AddForce(neg * speed, ForceMode2D.Impulse);
            pRb.AddForce(pos * speed, ForceMode2D.Impulse);

            negBullet.GetComponent<PlayerBullet>().SetValues(pierce, bStats[0], bStats[2]);
            posBullet.GetComponent<PlayerBullet>().SetValues(pierce, bStats[0], bStats[2]);
        }
        sfxPlayers[1].PlaySfx();
        StartCoroutine(AbilityCd(bStats[3]));
    }

    private void FireballRain()
    {
        float[] fStats = PlayerStats.Instance.GetFireball();

        for (int i = 0; i < Mathf.FloorToInt(fStats[1]) + 1; i++)
        {
            GameObject fBall = Instantiate(friendlyFireball, transform.position, Quaternion.identity);
            fBall.GetComponent<PlayerFireball>().bulletSpeed = 3f;
        }
        sfxPlayers[2].PlaySfx();
        StartCoroutine(AbilityCd(fStats[2]));
    }

    // Devouring
    private void Devour()
    {
        if (PlayerStats.Instance.hunger > 0) { return; }
        if (devourReady == false) { return; }
        
        PlayerStats.Instance.SetInvincible(true);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GetComponent<PlayerMovement>().ToggleCanMove();
        rb.velocity = Vector3.zero;
        rb.AddForce(cursorDiff * 250f, ForceMode2D.Impulse);

        devourReady = false;
        StartCoroutine(DevourCd());
    }

    IEnumerator DevourCd ()
    {
        sfxPlayers[4].PlaySfx();
        isDevouring = true;
        yield return new WaitForSeconds (0.2f);

        GetComponent<PlayerMovement>().ToggleCanMove();
        isDevouring = false;

        yield return new WaitForSeconds(1.5f);
        devourReady = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDevouring) // Devour is on cooldown
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                int newAbility = collision.collider.GetComponent<EnemyStats>().GetDevoured(400, transform.position, 20);
                isDevouring = false;
                OnDevourHit(newAbility);
                return;
            }
            PlayerStats.Instance.SetInvincible(false);
        }
    }

    IEnumerator EndInvincible (float time)
    {
        yield return new WaitForSeconds (time);
        PlayerStats.Instance.SetInvincible(false);
    }

    private void OnDevourHit(int ability)
    {
        Debug.Log("Devoured type: " + ability);
        if (ability == 0)
        {
            EndInvincible(0.5f);
        } else
        {
            attackType = ability;
            PlayerStats.Instance.DevourSuccess();
            StartCoroutine(Frenzy());
        }

    }

    IEnumerator Frenzy ()
    {
        isFrenzy = true;
        sfxPlayers[3].PlaySfx();
        float animTime = 0.25f;
        float frenzyTime = 5f;
        int whirlCount = Mathf.FloorToInt(frenzyTime / animTime); // 20 * 0,25sec = 5 seconds

        Debug.Log(whirlCount);

        for (int i = 0; i < whirlCount; i++)
        {
            Whirlwind();
            yield return new WaitForSeconds(animTime);
        }
        isFrenzy = false;
        EndInvincible(0.5f);
    }
}
