using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health; // Only if we want the health to fill out after changing. Good if we reduce max health, but we could do manually.
    }

    public void SetHealth (int health)
    {
        slider.value = health;
    }
}
