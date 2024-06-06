using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField]
    private float playerHealthCurrent;
    [SerializeField]
    private float playerHealthMax;

    public HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(playerHealthCurrent, playerHealthMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            float enemyDmg;

            enemyDmg = collision.gameObject.GetComponent<EnemyBehaviour>().AttackDamage;

            healthSystem.DmgTaken(enemyDmg);
        }
    }
}
