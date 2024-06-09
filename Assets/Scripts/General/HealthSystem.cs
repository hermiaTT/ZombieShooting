using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{

    //fields
    float currentHealth;
    float currentmaxHealth;
    public bool isDead = false;

    //properties
    public float Health
    {
        get
        { 
            return currentHealth;
        }
        set 
        { 
            currentHealth = value; 
        }
    }

    public float MaxHealth
    {
        get
        {
            return currentmaxHealth;
        }
        set
        {
            currentmaxHealth = value;
        }
    }

    public bool IsDead
    {
        get 
        {
            return isDead;
        }
        set 
        { 
            return;
        }
    }


    //constructor

    public HealthSystem(float health, float maxHealth) 
    { 
        currentHealth= health;
        currentmaxHealth= maxHealth;
    }

    //methods

    public void DeadCheck()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void DmgTaken(float damage) 
    { 
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
    }

    public void HealTaken(float healAmount)
    {
        if (currentHealth < currentmaxHealth)
        {
            currentHealth += healAmount;
        }
        if(currentHealth > currentmaxHealth) 
        { 
            currentHealth = currentmaxHealth;
        }
    }

}
