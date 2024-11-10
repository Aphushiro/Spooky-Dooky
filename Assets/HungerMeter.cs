using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerMeter : MonoBehaviour
{
    public GameObject playerStat;
    public int maxHunger = 2;
    private void Start()
    {
        int startWidth = (int)GetComponent<RectTransform>().rect.width;
    }
    private void FixedUpdate()
    {
        int hunger = playerStat.GetComponent<PlayerStats>().hunger;

        Debug.Log(hunger);
        Debug.Log(maxHunger);
        float ratio = (float)hunger / (float)maxHunger;
        Debug.Log(ratio);
        transform.localScale = new Vector3(ratio, 1, 1);
        
    }
}
