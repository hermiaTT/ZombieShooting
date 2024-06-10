using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    #region Player basic support

    //Player basic support
    [SerializeField]
    private float playerHealthCurrent;
    [SerializeField]
    private float playerHealthMax;

    HealthSystem healthSystem;
    //move support
    [SerializeField]
    private float speed =6f;
    #endregion

    #region WeaponSystem Support
    //Weapon System support
    bool canPickUp;
    GameObject itemNeedToPick;
    WeaponSystem weaponSystem;
    GameObject currentWeapon;
    #endregion

    #region Moving Support
    //control key support
    float horizonralMove;
    float verticalMove;
    #endregion

    #region Health Bar support
    //player health bar control
    [SerializeField]
    HealthBar healthBar;
    #endregion

    #region Animation Support

    //Player animation states
    Animator playerAnimator;
    string playerCurrentState;
    bool isHurt = false;
    bool isRun;
    const string PlAYER_IDLE = "Player_Idle";
    const string PlAYER_RUN = "Player_Run";
    const string PlAYER_DEAD = "Player_Dead";
    const string PlAYER_HURT = "Player_Hurt";
    //flip player support
    public bool facingRight = true;
    Vector3 currentScale;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        //weaponSystem = GetComponent<WeaponSystem>();


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
        else
        {
            isRun = false;
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

        #region Animation Control
        //Animation Control
        if (!isHurt && !healthSystem.isDead && !isRun)
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
        #endregion


        if (Input.GetKeyDown(KeyCode.Q))
        {
            WeaponSystem.Instance.SwitchActiveSlot();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            WeaponSystem.Instance.DropWeapon();
        }
        if (itemNeedToPick && canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            WeaponSystem.Instance.PickUpWeapon(itemNeedToPick.gameObject);
        }

    }

    #region Enemy Collision Detect

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
    #endregion

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        //if collision with weapon
        if (collision.gameObject.tag == ("Weapon"))
        {
            canPickUp = true;
            itemNeedToPick = collision.gameObject;
        }
    }

    #region Animation method

    //Player scale control
    void Flip()
    {
        //while use flip, the weapon on his hand should be flipped
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
    #endregion
}
