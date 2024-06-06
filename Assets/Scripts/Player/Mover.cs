using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Mover : MonoBehaviour
{
    //control key support
    float horizonralMove;
    float verticalMove;

   //move support
   [SerializeField]
    private float speed = 5f;

    //flip player support
    bool facingRight = true;
    Vector3 currentScale;

    

    //Player animation states
    Animator playerAnimator;
    string playerCurrentState;
    const string PlAYER_IDLE = "Player_Idle";
    const string PlAYER_RUN = "Player_Run";
    const string PlAYER_DEAD = "Player_dead";
    const string PlAYER_HURT = "Player_Hurt";

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
       
        currentScale = gameObject.transform.localScale;
    }


    // Update is called once per frame
    void Update()
    {
        //Movement method
        horizonralMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        Vector3 position = transform.position;
        if (horizonralMove != 0)
        {
            position.x += horizonralMove * speed * Time.deltaTime;
            ChangeAnimationState(PlAYER_RUN);
        }
        else if (verticalMove != 0)
        {
            position.y += verticalMove * speed * Time.deltaTime;
            ChangeAnimationState(PlAYER_RUN);
        }
        else 
        {
            ChangeAnimationState(PlAYER_IDLE);
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


    }

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
        if(newState == playerCurrentState)
        {
            return;
        }

        playerAnimator.Play(newState);
        playerCurrentState = newState;
    }



    //check if animation is playing
    bool IsAnimationPlaying(Animator animator, string stateName)
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

}


