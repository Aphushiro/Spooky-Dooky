using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float timer;
    public TMP_Text timerText;
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        timerText.text = Mathf.Floor(timer).ToString("F0");
    }
}
