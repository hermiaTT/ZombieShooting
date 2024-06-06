using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //flip player support
    bool facingRight = true;

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


    // Start is called before the first frame update
    void Start()
    {
        m_Transform = this.transform;


        // create and start timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = SpawnDelaySeconds;
        spawnTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation method
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        if (direction.x < 0 && facingRight)
        {

            Flip();
        }

        m_Transform.rotation = rotation;


        //spawn bullet
        if (spawnTimer.Finished)
        {
            //bullet spawn
            if (Input.GetAxis("Fire1") != 0)
            {
                GameObject temp = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
                temp.GetComponent<BulletController>().SetDirection(direction);
                temp.transform.rotation = rotation;
            }
            spawnTimer.Run();
        }


    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.y *= -1;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }


}
