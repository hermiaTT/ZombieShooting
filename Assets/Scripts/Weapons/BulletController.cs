using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    public float damage;
    [SerializeField]
    private float speed;
    private Vector2 direction;

    //detect collision
    bool hitTarget = false;

    //Animation control
    Animator animator;

    //Properties
    public float Damage
    {
        get
        {
            return damage;
        }
        set 
        { 
            damage = value;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = direction * speed * Time.deltaTime + (Vector2)transform.position;

        if(hitTarget) 
        {
            speed = 0;
            animator.Play("Bullet_Hit");
        }
        else 
        {
            animator.Play("Bullet");
        }

        //destroy bullet when hit something
    }

    //Set Bullet spawn direction
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    

    //method for collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")   hitTarget = true;
      
    }

    public void AnimationComplete()
    {
        Destroy(gameObject);
    }

}
