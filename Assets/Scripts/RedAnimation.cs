using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAnimation : MonoBehaviour
{
    
    float satVal = 0f;
    float satIncrease = 0.5f;
    public float satFadeTime = 1f;

    public SpriteRenderer spRend;

    private void Start()
    {
        spRend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (satVal > 0f)
        {
            satVal -= Time.deltaTime/satFadeTime;
        }
        float newSat = Mathf.Clamp01(satVal);

        spRend.color = Color.HSVToRGB(0, newSat, 1);
    }

    public void ActivateRedTintFeedback ()
    {
        satVal = satIncrease;
    }
}
