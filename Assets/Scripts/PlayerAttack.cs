using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    int attackType = 0; // Punch, 
    bool canAttack = true;
    Vector3 cursorDiff;

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        cursorDiff = mousePos - transform.position;
        cursorDiff.Normalize();
    }

    void Attack()
    {
        if (canAttack == false) { return; }

        switch (attackType)
        {
            case 0 :
                Punch();
                break;

            default:
                break;
        }
    }

    private void Punch()
    {
        float[] pStats = PlayerStats.Instance.GetPunch();
        float dist = (pStats[1] * 0.5f) + (transform.localScale.x / 2);

        Vector2 attackPos = transform.position + (cursorDiff * dist);

        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPos, pStats[1]);
    }

    private void OnDrawGizmos()
    {
        float[] pStats = PlayerStats.Instance.GetPunch();
        float dist = (pStats[1] * 0.5f) + (transform.localScale.x / 2);

        Vector2 attackPos = transform.position + (cursorDiff * dist);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(attackPos, pStats[1]);
    }
}
