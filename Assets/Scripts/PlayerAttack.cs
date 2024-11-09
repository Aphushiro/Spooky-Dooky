using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    Vector3 cursorDiff;
    Vector3 cursorRotation;

    bool canAttack = true;

    bool autoAttackBlock = false;
    public GameObject attackAnimPrefab;

    int attackType = 0; // None, Pitch, Musketeer, Torcher
    bool blockAbility = false;

    bool devourReady = true; // Independant of whether or not hunger is ready
    bool isDevouring = false;

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

    void AbilityMove()
    {
        if (canAttack == false) { return; }
        if (blockAbility) { return; }

        switch (attackType) // None, Pitch, Musketeer, Torcher
        {
            case 1 :
                Whirlwind();
                break;

            default:
                break;
        }
        blockAbility = true;
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
        yield return new WaitForSeconds(seconds);
        autoAttackBlock = false;
    }

    private void Punch()
    {
        float[] pStats = PlayerStats.Instance.GetPunch();               // { damage, range, knockback, cooldown }
        float dist = (pStats[1] * 0.5f) + (transform.localScale.x / 2);

        Vector2 attackPos = transform.position + (cursorDiff * dist);
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPos, pStats[1]);

        // Punch GFX
        Vector3 attackSize = new Vector3(pStats[1], pStats[1], 1f);
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
        StartCoroutine(AbilityCd(wStats[4]));
    }

    private void Devour()
    {
        if (PlayerStats.Instance.hunger > 0) { return; }
        if (devourReady == false) { return; }
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GetComponent<PlayerMovement>().ToggleCanMove();
        rb.velocity = Vector3.zero;
        rb.AddForce(cursorDiff * 250f, ForceMode2D.Impulse);

        devourReady = false;
        StartCoroutine(DevourCd());
    }

    IEnumerator DevourCd ()
    {
        isDevouring = true;
        yield return new WaitForSeconds (0.2f);

        GetComponent<PlayerMovement>().ToggleCanMove();
        isDevouring = false;

        yield return new WaitForSeconds(2f);
        devourReady = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDevouring) // Devour is on cooldown
        {
            if (collision.collider.CompareTag("Enemy"))
            {
                int newShape = collision.collider.GetComponent<EnemyStats>().GetDevoured(400, transform.position, 20);
            }
        }
    }
}
