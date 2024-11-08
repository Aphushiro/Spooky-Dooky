using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    int attackType = 0; // Punch, 
    bool canAttack = true;



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
        float[] pStats= PlayerStats.Instance.GetPunch();
    }
}
