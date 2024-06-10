using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //bool for is attacking
    public bool isAttack;
    [SerializeField]
    GameObject FireEffectPrefab;
    [SerializeField]
    Transform spawnPoint;


    //rotation support
    private Transform m_Transform;


    //bullet spawn frequency
    [SerializeField]
    private float SpawnDelaySeconds;
    Timer spawnTimer;

    //spawn information support
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform muzzle;


    public Vector2 spawnPosition;
    //item picked up
    public bool isActiveWeapon;
    BoxCollider2D collisionSet;

    //Weapon Drop support
    bool drop;
    public float throwSpeed = 0.5f;
    PlayerBehaviour playerBehaviour;
    Rigidbody2D rb2d;

    public bool IsAttack
    {
        get { return isAttack; }
    }



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        collisionSet = gameObject.GetComponent<BoxCollider2D>();

        playerBehaviour = GetComponent<PlayerBehaviour>();

        m_Transform = this.transform;

        // create and start timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = SpawnDelaySeconds;
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {

        if (isActiveWeapon) 
        {
            #region Weapon Rotation
            //Rotation method
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = direction.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x = GameObject.FindWithTag("Player").transform.localScale.x;
            currentScale.y = currentScale.x == 1 ? 1 : -1;
            gameObject.transform.localScale = currentScale;

            m_Transform.rotation = rotation;
            #endregion

            #region Bullet Spawn
            //spawn bullet
            if (spawnTimer.Finished)
            {
                //bullet spawn
                if (Input.GetAxis("Fire1") != 0)
                {
                    GameObject temp = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
                    temp.GetComponent<BulletController>().SetDirection(direction);
                    temp.transform.rotation = rotation;

                    isAttack = true;
                }
                else
                {
                    isAttack = false;
                }
                spawnTimer.Run();

                if (isAttack)
                {
                    Instantiate(FireEffectPrefab, muzzle.position, Quaternion.identity);
                }
            }
            #endregion         
        }


    }
    public void DropWeapon()
    {
        gameObject.transform.SetParent(null);
        gameObject.transform.position = GameObject.FindWithTag("Player").transform.position;
        gameObject.transform.localRotation = Quaternion.identity;
        //set rotation to 0, and set scale to the direction when dropped
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = new Vector3(GameObject.FindWithTag("Player").transform.localScale.x, 1, 1);

        //add force to drop
        Vector3 currentScale = gameObject.transform.localScale;
        rb2d.velocity = new Vector3(currentScale.x * throwSpeed, -currentScale.y, currentScale.z);
        rb2d.AddForce(rb2d.velocity, ForceMode2D.Impulse);
        //set direction to the direction it is dropped
        isActiveWeapon = false;

    }

    public void ActiveWeapon(GameObject parentSlot, Transform spwanPosition)
    {
        gameObject.transform.SetParent(parentSlot.transform, false);
        gameObject.transform.position = spwanPosition.position;
    }


}
