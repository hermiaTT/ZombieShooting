using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    //Player basic support
    [SerializeField]
    private float playerHealthCurrent;
    [SerializeField]
    private float playerHealthMax;

    HealthSystem healthSystem;

    //control key support
    float horizonralMove;
    float verticalMove;

    //move support
    [SerializeField]
    private float speed = 5f;

    //flip player support
    bool facingRight = true;
    Vector3 currentScale;


    //player health bar control
    [SerializeField]
    HealthBar healthBar;

    //Player animation states
    Animator playerAnimator;
    string playerCurrentState;
    bool isHurt = false;
    bool isRun;
    const string PlAYER_IDLE = "Player_Idle";
    const string PlAYER_RUN = "Player_Run";
    const string PlAYER_DEAD = "Player_Dead";
    const string PlAYER_HURT = "Player_Hurt";

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(playerHealthCurrent, playerHealthMax);

        playerAnimator = gameObject.GetComponent<Animator>();

        currentScale = gameObject.transform.localScale;

        
    }

    // Update is called once per frame
    void Update()
    {

        #region Moving Method

        //Movement method
        horizonralMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        Vector3 position = transform.position;
        if (horizonralMove != 0)
        {
            position.x += horizonralMove * speed * Time.deltaTime;
            isRun = true;
        }
        else if (verticalMove != 0)
        {
            position.y += verticalMove * speed * Time.deltaTime;
            isRun = true;
        }
        transform.position = position;

        //Player flip
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        if (direction.x < 0 && facingRight)
        {
            Flip();

        }
        #endregion

        healthSystem.DeadCheck();

        //Animation Control
        if (!isHurt && !healthSystem.IsDead && !isRun)
        {
            playerAnimator.Play(PlAYER_IDLE);
        }
        else if (!isHurt && !healthSystem.IsDead && isRun)
        {
            playerAnimator.Play(PlAYER_RUN);
        }
        else if (isHurt && !healthSystem.IsDead)
        {
            playerAnimator.Play(PlAYER_HURT);
        }
        else if (healthSystem.IsDead)
        {
            playerAnimator.Play(PlAYER_DEAD);
        }


    }



    //Player collision detect
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            float enemyDmg;

            enemyDmg = collision.gameObject.GetComponent<EnemyBehaviour>().AttackDamage;

            healthSystem.DmgTaken(enemyDmg);

            Debug.Log(healthSystem.Health);

            healthBar.SetHealth(healthSystem.Health);

            isHurt = true;
        }
    }

    //Player scale control
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
        if (newState == playerCurrentState)
        {
            return;
        }

        playerAnimator.Play(newState);
        playerCurrentState = newState;
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

    //HurtAnimation complete event check
    public void HurtAnimComplete()
    {
        isHurt = false;

    }

    public void FreezePlayer()
    {
        speed = 0;
    }

    public void DeadAnimationComplete()
    {
        Destroy(gameObject);
    }
}
