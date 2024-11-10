using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartScrip : MonoBehaviour
{
    public int heartnumber;
    public GameObject playerStats;
    private void FixedUpdate()
    {
        if (playerStats.GetComponent<PlayerStats>().currentHealth < heartnumber)
        {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 0); // Set color to transparent
        }
    }
}
