using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    //fileds
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float health;

    //properties
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public float AttackDamage
    {
        get
        {
            return attackDamage;
        }
        set
        {
            attackDamage = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }




    //damage taken support
    public HealthSystem healthSystem;


    //chasing support
    private float distance;
    [SerializeField] public float stopDistance;
    private bool chasing = true;


    //flip support
    bool facingRight = true;
    Vector3 currentScale;

    //Animation support
    Animator enemyAnimator;
    string enemyCurrentState;
    bool gethurt = false;
    const string ZOMBIE_IDLE = "Zombie_Idle";
    const string ZOMBIE_RUN = "Zombie_Run";
    const string ZOMBIE_DEAD = "Zombie_Dead";
    const string ZOMBIE_HURT = "Zombie_Hurt";
   


    // Start is called before the first frame update
    void Start()
    {
        //assign health data
        healthSystem = new HealthSystem(health, health);

        //animation support
        enemyAnimator = gameObject.GetComponent<Animator>();
    }

    //
    void Update()
    {

        //moving method
        //chasing method
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < stopDistance)
        {
            chasing = false;
        }
        else
        {
            chasing = true;
        }
        if (chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        //sprite flip
        if (player.transform.position.x < transform.position.x && facingRight) Flip();

        if (player.transform.position.x > transform.position.x && !facingRight) Flip();
    



        //dead check
        healthSystem.DeadCheck();

        //animation control
       if(!gethurt && !healthSystem.IsDead && !chasing) 
        {
            enemyAnimator.Play(ZOMBIE_IDLE);
        }
       else if(!gethurt && !healthSystem.IsDead && chasing)
        {
            enemyAnimator.Play(ZOMBIE_RUN);
        }
       else if(gethurt && !healthSystem.IsDead)
        {
            enemyAnimator.Play(ZOMBIE_HURT);
        }
       else if(healthSystem.IsDead) 
        { 
            enemyAnimator.Play(ZOMBIE_DEAD);
        }


    }

    //sprite flip method
    void Flip()
    {
        currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;

    }



    //change animation state
    private void ChangeAnimationState(string newState)
    {
        if (newState == enemyCurrentState)
        {
            return;
        }
        enemyAnimator.Play(newState);
        enemyCurrentState = newState;
    }

    //check if animation is playing
    bool IsAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == ("Bullet"))
        {
            float bulletDmg;

            bulletDmg = collision.gameObject.GetComponent<BulletController>().Damage;

            healthSystem.DmgTaken(bulletDmg);
            gethurt = true;
            
        }
    }


    //animation event
    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    public void HurtAnimComplete()
    {
        gethurt = false;
        
    }
       
}
