using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public GameObject playerStats;
    private void FixedUpdate()
    {
        if (playerStats.GetComponent<PlayerStats>().currentHealth > 0)
        {
            timer += Time.deltaTime;
            timerText.text = Mathf.Floor(timer).ToString("F0");
            scoreText.text = "You survived for " + Mathf.Floor(timer).ToString("F0") + " seconds!";
        }
    }
}
