using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    //health bar control support
    Slider healthSlider;

    void Start()
    {
       healthSlider = gameObject.GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxhealth)
    {
        healthSlider.maxValue = maxhealth;
        healthSlider.value = maxhealth;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }




}
